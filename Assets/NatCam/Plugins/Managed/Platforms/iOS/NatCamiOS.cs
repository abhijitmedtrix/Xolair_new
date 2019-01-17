/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using AOT;
    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;
    using NatRenderU;

    public sealed class NatCamiOS : INatCam {

        #region --Op vars--
        private Texture2D preview;
        private Action startCallback, frameCallback;
        private Action<Texture2D> photoCallback;
        private static NatCamiOS instance { get { return NatCam.Implementation as NatCamiOS; }}
        #endregion
        

        #region --Properties--
        public bool HasPermissions { get { return NatCamBridge.HasPermissions(); }}
        public int CameraCount { get { return NatCamBridge.CameraCount(); }}
        public DeviceCamera Camera { get { return NatCamBridge.GetCamera(); }}
        public Texture Preview { get { return preview; }}
        public bool IsRunning { get { return NatCamBridge.IsRunning(); }}
        #endregion


        #region --Ctor--

        public NatCamiOS () {
            NatCamBridge.RegisterCoreCallbacks(OnStart, OnFrame, OnPhoto, null);
            OrientationUtility.onOrient += OnOrient;
            Debug.Log("NatCam: Initialized NatCam 2.1 iOS backend");
        }
        #endregion
        

        #region --Operations--

        public void StartPreview (DeviceCamera camera, Action startCallback, Action frameCallback) {
            this.startCallback = startCallback;
            this.frameCallback = frameCallback;
            NatCamBridge.StartPreview((IntPtr)(camera as DeviceCameraiOS));
            OnOrient();
        }

        public void StopPreview () {
            NatCamBridge.StopPreview();
            Texture2D.Destroy(preview);
            preview = null;
            startCallback = 
            frameCallback = null;
        }

        public void CapturePhoto (Action<Texture2D> callback) {
            photoCallback = callback;
            NatCamBridge.CapturePhoto();
        }

        public void CaptureFrame (byte[] dest) {
            NatCamBridge.CaptureFrame(dest);
        }
        #endregion


        #region --Callbacks--

        [MonoPInvokeCallback(typeof(NatCamBridge.StartCallback))]
        private static void OnStart (IntPtr texPtr, int width, int height) {
            if (!instance.preview)
                instance.preview = Texture2D.CreateExternalTexture(width, height, TextureFormat.RGBA32, false, false, texPtr);
            if (instance.preview.width != width || instance.preview.height != height)
                instance.preview.Resize(width, height, instance.preview.format, false);
            instance.preview.UpdateExternalTexture(texPtr);
            instance.startCallback();
        }

        [MonoPInvokeCallback(typeof(NatCamBridge.PreviewCallback))]
        private static void OnFrame (IntPtr texPtr, long timestamp) {
            if (instance.preview == null)
                return;
            instance.preview.UpdateExternalTexture(texPtr);
            if (instance.frameCallback != null)
                instance.frameCallback();
        }
        
        [MonoPInvokeCallback(typeof(NatCamBridge.PhotoCallback))]
        private static void OnPhoto (IntPtr imgPtr, int width, int height) {
            var data = new byte[width * height * 4];
            Marshal.Copy(imgPtr, data, 0, data.Length);
            using (var dispatch = Dispatcher.Create(DispatchThread.MainThread))
                dispatch.Dispatch(() => {
                    var photo = new Texture2D(width, height, TextureFormat.BGRA32, false);
                    photo.LoadRawTextureData(data);
                    photo.Apply();
                    instance.photoCallback(photo);
                    instance.photoCallback = null;
                });
        }
        #endregion


        #region --Utility--

        private void OnOrient () {
            NatCamBridge.OnOrient((int)OrientationUtility.Orientation);
        }
        #endregion
    }
}
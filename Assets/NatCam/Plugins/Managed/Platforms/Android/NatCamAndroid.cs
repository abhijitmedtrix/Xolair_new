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
    using NatRenderU.Dispatch;

    public sealed class NatCamAndroid : AndroidJavaProxy, INatCam {

        #region --Op vars--
        private Texture2D preview;
        private Dispatcher dispatcher;
        private Action startCallback, frameCallback;
        private Action<Texture2D> photoCallback;
        private int previewCameraOnAppPause;
        private readonly AndroidJavaObject renderDispatch;
        private readonly AndroidJavaObject natcam;
        #endregion


        #region --Properties--
        public bool HasPermissions { get { return natcam.Call<bool>("hasPermissions"); }}
        public int CameraCount { get { return natcam.Call<int>("cameraCount"); }}
        public DeviceCamera Camera {
            get {
                try {
                    using (var activeCamera = natcam.Get<AndroidJavaObject>("activeCamera"))
                        return activeCamera.Get<int>("index");
                } catch (Exception) {
                    return -1;
                }
            }
        }
        public Texture Preview { get { return preview; }}
        public bool IsRunning { get { return natcam.Call<bool>("isRunning"); }}
        #endregion


        #region --Ctor--

        public NatCamAndroid () : base("com.yusufolokoba.natcam.NatCamDelegate") {
            renderDispatch = new AndroidJavaObject("com.yusufolokoba.natrender.RenderDispatch");
            natcam = new AndroidJavaObject("com.yusufolokoba.natcam.NatCam", this, renderDispatch);
            var renderDispatcher = Dispatcher.Create(DispatchThread.RenderThread);
            DispatchUtility.onFrame += () => renderDispatcher.Dispatch(() => renderDispatch.Call("invoke"));
            dispatcher = Dispatcher.Create(DispatchThread.MainThread);
            DispatchUtility.onPause += OnPause;
            OrientationUtility.onOrient += OnOrient;
            Debug.Log("NatCam: Initialized NatCam 2.1 Android backend");
        }
        #endregion
        

        #region --Operations--

        public void StartPreview (DeviceCamera camera, Action startCallback, Action frameCallback) {
            this.startCallback = startCallback;
            this.frameCallback = frameCallback;
            OnOrient();
            natcam.Call("startPreview", (AndroidJavaObject)(camera as DeviceCameraAndroid));
        }

        public void StopPreview () {
            natcam.Call("stopPreview");
            Texture2D.Destroy(preview);
            preview = null;
            // Don't nullify the callbacks because they are used by `OnPause`
        }

        public void CapturePhoto (Action<Texture2D> callback) {
            photoCallback = callback;
            natcam.Call("capturePhoto");
        }
        public void CaptureFrame (byte[] dest) {
            var handle = GCHandle.Alloc(dest, GCHandleType.Pinned);
            natcam.Call("captureFrame", handle.AddrOfPinnedObject().ToInt64());
            handle.Free();
        }
        #endregion


        #region --Callbacks--

        private void onStart (int texPtr, int width, int height) {
            dispatcher.Dispatch(() => {
                preview = preview ?? Texture2D.CreateExternalTexture(width, height, TextureFormat.RGBA32, false, false, (IntPtr)texPtr);
                if (preview.width != width || preview.height != height)
                    preview.Resize(width, height, preview.format, false);
                preview.UpdateExternalTexture((IntPtr)texPtr);
                startCallback();
            });
        }

        private void onFrame (int texPtr, long timestamp) {
            dispatcher.Dispatch(() => {
                if (preview == null)
                    return;
                preview.UpdateExternalTexture((IntPtr)texPtr);
                if (frameCallback != null)
                    frameCallback();
            });
        }

        private void onPhoto (long buffer, long size, int width, int height) {
            byte[] data = new byte[size];
            Marshal.Copy((IntPtr)buffer, data, 0, data.Length);
            dispatcher.Dispatch(() => {
                var photo = new Texture2D(width, height, TextureFormat.RGBA32, false);
                photo.LoadRawTextureData(data);
                photo.Apply();
                photoCallback(photo);
                photoCallback = null;
            });
        }
        #endregion


        #region --Utility--
        
        private void OnPause (bool paused) {
            previewCameraOnAppPause = paused ? (int)Camera : previewCameraOnAppPause;
            if (paused) {
                if (IsRunning)
                    StopPreview();
            }
            else {
                if (previewCameraOnAppPause > -1)
                    StartPreview(previewCameraOnAppPause, startCallback, frameCallback);
            }
        }

        private void OnOrient () {
            natcam.Call("onOrient", (int)OrientationUtility.Orientation);
        }
        #endregion
    }
}
/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;
    using NatRenderU.Dispatch;

    public sealed class NatCamLegacy : INatCam {

        #region --Op vars--
        private DeviceCameraLegacy camera;
        private bool firstFrame;
        private Action startCallback, frameCallback;
        private Color32[] previewBuffer;
        #endregion


        #region --Properties--
        public bool HasPermissions { get { return true; }}
        public int CameraCount { get { return WebCamTexture.devices.Length; }}
        public DeviceCamera Camera { get { return camera; }}
        public WebCamTexture PreviewTexture { get; private set; }
        public Texture Preview { get { return PreviewTexture; }}
        public bool IsRunning { get { return PreviewTexture && PreviewTexture.isPlaying; }}
        #endregion
        

        #region --Operations--

        public NatCamLegacy () {
            Debug.Log("NatCam: Initialized NatCam 2.1 Legacy backend");
        }

        public void StartPreview (DeviceCamera genericCamera, Action startCallback, Action frameCallback) {
            this.startCallback = startCallback;
            this.frameCallback = frameCallback;
            var camera = genericCamera as DeviceCameraLegacy;
            // Create preview
            if (PreviewTexture && this.camera != camera) {
                WebCamTexture.Destroy(PreviewTexture);
                PreviewTexture = null;
            }
            if (!PreviewTexture) {
                if (camera.PreviewResolution.x == 0)
                    PreviewTexture = new WebCamTexture(camera.Device.name);
                else
                    PreviewTexture = new WebCamTexture(
                        camera.Device.name,
                        camera.PreviewResolution.x,
                        camera.PreviewResolution.y,
                        (int)Mathf.Max(camera.Framerate, 30)
                    );
                this.camera = camera as DeviceCameraLegacy;
            }
            // Start preview
            firstFrame = true;
            PreviewTexture.Play();
            DispatchUtility.onFrame += Update;
        }

        public void StopPreview () {
            DispatchUtility.onFrame -= Update;
            PreviewTexture.Stop();
            WebCamTexture.Destroy(PreviewTexture);
            PreviewTexture = null;
            previewBuffer = null;
            camera = null;
            startCallback =
            frameCallback = null;
        }

        public void CapturePhoto (Action<Texture2D> callback) {
            var photo = new Texture2D(PreviewTexture.width, PreviewTexture.height, TextureFormat.RGB24, false, false);
            photo.SetPixels32(PreviewTexture.GetPixels32());
            photo.Apply();
            callback(photo);
        }

        public void CaptureFrame (byte[] dest) {
            // Copy
            GCHandle pin = GCHandle.Alloc(previewBuffer, GCHandleType.Pinned);
            Marshal.Copy(pin.AddrOfPinnedObject(), dest, 0, previewBuffer.Length * Marshal.SizeOf(typeof(Color32)));
            pin.Free();
        }
        #endregion


        #region --State Management--

        private void Update () {
            // Check that we are playing
            if (
                !PreviewTexture.didUpdateThisFrame ||
                PreviewTexture.width == 16 || 
                PreviewTexture.height == 16
            ) return;
            // Update preview buffer
            if (previewBuffer == null)
                previewBuffer = PreviewTexture.GetPixels32();
            else
                PreviewTexture.GetPixels32(previewBuffer);
            // Invoke events
            if (firstFrame) {
                startCallback();
                firstFrame = false;
            }
            if (frameCallback != null)
                frameCallback();
        }
        #endregion
    }
}
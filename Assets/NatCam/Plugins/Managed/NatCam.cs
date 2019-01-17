/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core {

    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;
    using Platforms;
    using Docs;

    [Doc(@"NatCam")]
    public static class NatCam {

        #region --Properties--

        /// <summary>
        /// The backing implementation NatCam uses on this platform
        /// </summary>
        [Doc(@"Implementation")]
        public static readonly INatCam Implementation;
        /// <summary>
        /// The camera preview as a Texture
        /// </summary>
        [Doc(@"Preview")]
        public static Texture Preview { get { return Implementation.Preview; }}
        /// <summary>
        /// Get the active camera
        /// </summary>
        [Doc(@"Camera")]
        public static DeviceCamera Camera { get { return Implementation.Camera; }}
        /// <summary>
        /// Is the preview running?
        /// </summary>
        [Doc(@"IsRunning")]
        public static bool IsRunning { get { return Implementation.IsRunning; }}
        #endregion


        #region --Operations--

        /// <summary>
        /// Start the camera preview
        /// </summary>
        /// <param name="camera">Camera that the preview should start from</param>
        /// <param name="startCallback">Callback invoked when the preview starts</param>
        /// <param name="frameCallback">Optional. Callback invoked when a new preview frame is available</param>
        [Doc(@"StartPreview")]
        public static void StartPreview (DeviceCamera camera, Action startCallback, Action frameCallback = null) {
            if (!camera) {
                Debug.LogError("NatCam Error: Cannot start preview because camera is null");
                return;
            }
            Implementation.StartPreview(camera, startCallback, frameCallback);
        }

        /// <summary>
        /// Stop the camera preview
        /// </summary>
        [Doc(@"StopPreview")]
        public static void StopPreview () {
            if (!IsRunning) {
                Debug.LogError("NatCam Error: Cannot stop preview because preview is not running");
                return;
            }
            Implementation.StopPreview();
        }

        /// <summary>
        /// Capture a photo
        /// </summary>
        /// <param name="callback">The callback to be invoked when NatCam receives the captured photo</param>
        [Doc(@"CapturePhoto", @"CapturePhotoDiscussion"), Code(@"TakeAPhoto")]
        public static void CapturePhoto (Action<Texture2D> callback) {
            if (callback == null) {
                Debug.LogError("NatCam Error: Cannot capture photo when callback is null");
                return;
            }
            if (!IsRunning) {
                Debug.LogError("NatCam Error: Cannot capture photo when session is not running");
                return;
            }
            Implementation.CapturePhoto(callback);
        }

        /// <summary>
        /// Capture the current preview frame.
        /// This function is less efficient than the `byte[]` overload because it allocates memory.
        /// This function is not thread safe.
        /// </summary>
        /// <param name="frame">Destination texture</param>
        [Doc(@"CaptureFrame", @"CaptureFrameDiscussion")]
        public static void CaptureFrame (Texture2D frame) {
            // Check
            if (!IsRunning) {
                Debug.LogError("NatCam Error: Cannot capture frame when preview is not running");
                return;
            }
            if (!frame) {
                Debug.LogError("NatCam Error: Cannot capture frame to null texture");
                return;
            }
            if (frame.width != Preview.width || frame.height != Preview.height) {
                Debug.LogError("NatCam Error: Cannot capture frame because texture size does not  match that of NatCam.Preview");
                return;
            }
            if (frame.format != TextureFormat.RGBA32)
                Debug.LogWarning("NatCam: Capture frame input texture format should be RGBA32");
            // Pass to implementation
            var pixelBuffer = new byte[Preview.width * Preview.height * 4];
            CaptureFrame(pixelBuffer);
            frame.LoadRawTextureData(pixelBuffer);
            frame.Apply();
        }

        /// <summary>
        /// Capture the current preview frame.
        /// The preview data is copied into the provided byte array.
        /// This function is thread-safe.
        /// </summary>
        /// <param name="pixels">Destination pixel buffer</param>
        [Doc(@"CaptureFrameData", @"CaptureFrameDataDiscussion"), Code(@"OpenCVMat")]
        public static void CaptureFrame (byte[] pixels) {
            // Check
            if (!IsRunning) {
                Debug.LogError("NatCam Error: Cannot capture frame when preview is not running");
                return;
            }
            if (pixels == null) {
                Debug.LogError("NatCam Error: Cannot capture frame to null pixel buffer");
                return;
            }
            // Pass to implementation
            Implementation.CaptureFrame(pixels);
        }
        #endregion


        #region --Initialization--

        static NatCam () {
            // Instantiate implementation for this platform
            Implementation =
            #if UNITY_EDITOR || UNITY_STANDALONE
            new NatCamLegacy();
            #elif UNITY_IOS
            new NatCamiOS();
            #elif UNITY_ANDROID
            new NatCamAndroid();
            #else
            new NatCamLegacy();
            #endif
        }
        #endregion
    }
}
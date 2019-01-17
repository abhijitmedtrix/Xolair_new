/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core {

    using UnityEngine;
    using System.Linq;
    using Platforms;
    using Docs;

    [Doc(@"DeviceCamera")]
    public abstract class DeviceCamera {

        #region ---Statics---
        /// <summary>
        /// The default front camera
        /// </summary>
        [Doc(@"FrontCamera")]
        public static readonly DeviceCamera FrontCamera;
        /// <summary>
        /// The default rear camera
        /// </summary>
		[Doc(@"RearCamera")]
        public static readonly DeviceCamera RearCamera;
        /// <summary>
        /// All cameras on the device
        /// </summary>
        [Doc(@"Cameras"), Code(@"EnableTorch")]
        public static readonly DeviceCamera[] Cameras; // You shall not touch!
        #endregion


        #region --Getters--
        /// <summary>
        /// Is the camera front facing?
        /// </summary>
        [Doc(@"IsFrontFacing")]
        public abstract bool IsFrontFacing { get; }
        /// <summary>
        /// Does this camera support flash?
        /// </summary>
        [Doc(@"IsFlashSupported")]
        public abstract bool IsFlashSupported { get; }
        /// <summary>
        /// Does this camera support torch?
        /// </summary>
        [Doc(@"IsTorchSupported")]
        public abstract bool IsTorchSupported { get; }
        /// <summary>
        /// Get the camera's horizontal field-of-view
        /// </summary>
        [Doc(@"HorizontalFOV")]
        public abstract float HorizontalFOV { get; }
        /// <summary>
        /// Get the camera's vertical field-of-view
        /// </summary>
        [Doc(@"VerticalFOV")]
        public abstract float VerticalFOV { get; }
        /// <summary>
        /// Get the camera's minimum exposure bias
        /// </summary>
        [Doc(@"MinExposureBias")]
        public abstract float MinExposureBias { get; }
        /// <summary>
        /// Get the camera's maximum exposure bias
        /// </summary>
        [Doc(@"MaxExposureBias")]
        public abstract float MaxExposureBias { get; }
        /// <summary>
        /// Get the camera's maximum zoom ratio
        /// </summary>
        [Doc(@"MaxZoomRatio")]
        public abstract float MaxZoomRatio { get; }
        #endregion


        #region ---Properties---
        /// <summary>
        /// Get or set the current preview resolution of the camera
        /// </summary>
        [Doc(@"PreviewResolution")]
        public abstract Vector2Int PreviewResolution { get; set; }
        /// <summary>
        /// Get or set the current photo resolution of the camera
        /// </summary>
        [Doc(@"PhotoResolution")]
        public abstract Vector2Int PhotoResolution { get; set; }
        /// <summary>
        /// Get or set the current framerate of the camera
        /// </summary>
        [Doc(@"Framerate")]
        public abstract float Framerate { get; set; }
        /// <summary>
        /// Get or set the camera's exposure lock
        /// </summary>
        [Doc(@"ExposureLock")]
        public abstract bool ExposureLock { get; set; }
        /// <summary>
        /// Get or set the camera's exposure bias
        /// </summary>
        [Doc(@"ExposureBias", @"ExposureBiasDiscussion")]
        public abstract float ExposureBias { get; set; }
        /// <summary>
        /// Get or set the camera's flash mode when taking a picture
        /// </summary>
        [Doc(@"CameraFlashMode")]
        public abstract FlashMode FlashMode { get; set; }
        /// <summary>
        /// Get or set the camera's focus lock
        /// </summary>
        [Doc(@"FocusLock")]
        public abstract bool FocusLock { get; set; }
        /// <summary>
        /// Set the camera's focus point of interest
        /// </summary>
        //[Doc(@"FocusPoint", @"FocusPointDiscussion"), Code(@"FocusCamera")]
        public abstract Vector2 FocusPoint { set; }
        /// <summary>
        /// Get or set the camera's torch mode
        /// </summary>
        [Doc(@"TorchEnabled")]
        public abstract bool TorchEnabled { get; set; }
        /// <summary>
        /// Get or set the camera's white balance lock
        /// </summary>
        [Doc(@"WhiteBalanceLock")]
        public abstract bool WhiteBalanceLock { get; set; }
        /// <summary>
        /// Get or set the camera's current zoom ratio. This value must be between [1, MaxZoomRatio]
        /// </summary>
        [Doc(@"ZoomRatio")]
        public abstract float ZoomRatio { get; set; }
        #endregion


        #region ---Typecasting---
		public static implicit operator int (DeviceCamera cam) { return cam ? cam.index : -1; }
        public static implicit operator DeviceCamera (int index) { return index >= 0 && index < Cameras.Length ? Cameras[index] : null; }
        public static implicit operator bool (DeviceCamera cam) { return cam != null; }
        public override string ToString () { return index.ToString(); }
        #endregion


        #region ---Intializers---

        private readonly int index;

        protected DeviceCamera (int i) { index = i; }

        static DeviceCamera () {
            Cameras = new DeviceCamera[NatCam.Implementation.CameraCount];
            for (int i = 0; i < Cameras.Length; i++)
                Cameras[i] =
                #if UNITY_EDITOR || UNITY_STANDALONE
                new DeviceCameraLegacy(i);
                #elif UNITY_IOS
                new DeviceCameraiOS(i);
                #elif UNITY_ANDROID
                new DeviceCameraAndroid(i);
                #else
                new DeviceCameraLegacy(i);
                #endif
            RearCamera = Cameras.FirstOrDefault(c => !c.IsFrontFacing);
            FrontCamera = Cameras.FirstOrDefault(c => c.IsFrontFacing);
        }
        #endregion
    }
}
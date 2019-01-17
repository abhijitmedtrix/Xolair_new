/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using System;
    using System.Runtime.InteropServices;

    public static class DeviceBridge {

        private const string Assembly =
        #if UNITY_IOS
        "__Internal";
        #else
        "NatCam";
        #endif

        #if UNITY_IOS && !UNITY_EDITOR

        [DllImport(Assembly, EntryPoint = "NCCoreAcquireCamera")]
        public static extern IntPtr AcquireCamera (int index);

        #region --Properties--
        [DllImport(Assembly, EntryPoint = "NCCoreIsFrontFacing")]
        public static extern bool IsFrontFacing (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreIsFlashSupported")]
        public static extern bool IsFlashSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreIsTorchSupported")]
        public static extern bool IsTorchSupported (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreHorizontalFOV")]
        public static extern float HorizontalFOV (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreVerticalFOV")]
        public static extern float VerticalFOV (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreMinExposureBias")]
        public static extern float MinExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreMaxExposureBias")]
        public static extern float MaxExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreMaxZoomRatio")]
        public static extern float MaxZoomRatio (this IntPtr camera);
        #endregion


        #region --Getters--

        [DllImport(Assembly, EntryPoint = "NCCorePreviewResolution")]
        public static extern void PreviewResolution (this IntPtr camera, out int width, out int height);
        [DllImport(Assembly, EntryPoint = "NCCorePhotoResolution")]
        public static extern void PhotoResolution (this IntPtr camera, out int width, out int height);
        [DllImport(Assembly, EntryPoint = "NCCoreFramerate")]
        public static extern float Framerate (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreExposureBias")]
        public static extern float ExposureBias (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreExposureLock")]
        public static extern bool ExposureLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreFocusLock")]
        public static extern bool FocusLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreFlashMode")]
        public static extern FlashMode FlashMode (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreTorchEnabled")]
        public static extern bool TorchEnabled (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreWhiteBalanceLock")]
        public static extern bool WhiteBalanceLock (this IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreZoomRatio")]
        public static extern float ZoomRatio (this IntPtr camera);
        #endregion


        #region --Setters--
        [DllImport(Assembly, EntryPoint = "NCCoreSetPreviewResolution")]
        public static extern void SetPreviewResolution (this IntPtr camera, int width, int height);
        [DllImport(Assembly, EntryPoint = "NCCoreSetPhotoResolution")]
        public static extern void SetPhotoResolution (this IntPtr camera, int width, int height);
        [DllImport(Assembly, EntryPoint = "NCCoreSetFramerate")]
        public static extern void SetFramerate (this IntPtr camera, float framerate);
        [DllImport(Assembly, EntryPoint = "NCCoreSetFocusPoint")]
        public static extern void SetFocusPoint (this IntPtr camera, float x, float y);
        [DllImport(Assembly, EntryPoint = "NCCoreSetExposureBias")]
        public static extern void SetExposureBias (this IntPtr camera, float bias);
        [DllImport(Assembly, EntryPoint = "NCCoreSetFocusLock")]
        public static extern void SetFocusLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = "NCCoreSetExposureLock")]
        public static extern void SetExposureLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = "NCCoreSetFlashMode")]
        public static extern void SetFlashMode (this IntPtr camera, FlashMode state);
        [DllImport(Assembly, EntryPoint = "NCCoreSetTorchEnabled")]
        public static extern void SetTorchEnabled (this IntPtr camera, bool enabled);
        [DllImport(Assembly, EntryPoint = "NCCoreSetWhiteBalanceLock")]
        public static extern void SetWhiteBalanceLock (this IntPtr camera, bool locked);
        [DllImport(Assembly, EntryPoint = "NCCoreSetZoomRatio")]
        public static extern void SetZoomRatio (this IntPtr camera, float ratio);
        #endregion


        #else
        public static IntPtr AcquireCamera (int index) { return IntPtr.Zero; }
        public static bool IsFrontFacing (this IntPtr camera) { return true; }
        public static bool IsFlashSupported (this IntPtr camera) { return false; }
        public static bool IsTorchSupported (this IntPtr camera) { return false; }
        public static float HorizontalFOV (this IntPtr camera) { return 0; }
        public static float VerticalFOV (this IntPtr camera) { return 0; }
        public static float MinExposureBias (this IntPtr camera) { return 0; }
        public static float MaxExposureBias (this IntPtr camera) { return 0; }
        public static float MaxZoomRatio (this IntPtr camera) { return 1; }
        public static void PreviewResolution (this IntPtr camera, out int width, out int height) { width = height = 0; }
        public static void PhotoResolution (this IntPtr camera, out int width, out int height) { width = height = 0; }
        public static float Framerate (this IntPtr camera) { return 0; }
        public static float ExposureBias (this IntPtr camera) { return 0; }
        public static bool ExposureLock (this IntPtr camera) { return false; }
        public static bool FocusLock (this IntPtr camera) { return false; }
        public static FlashMode FlashMode (this IntPtr camera) { return 0; }
        public static bool TorchEnabled (this IntPtr camera) { return false; }
        public static bool WhiteBalanceLock (this IntPtr camera) { return false; }
        public static float ZoomRatio (this IntPtr camera) {return 0;}
        public static void SetPreviewResolution (this IntPtr camera, int width, int height) {}
        public static void SetPhotoResolution (this IntPtr camera, int width, int height) {}
        public static void SetFramerate (this IntPtr camera, float framerate) {}
        public static void SetFocusPoint (this IntPtr camera, float x, float y) {}
        public static void SetExposureBias (this IntPtr camera, float bias) {}
        public static void SetFocusLock (this IntPtr camera, bool locked) {}
        public static void SetExposureLock (this IntPtr camera, bool locked) {}
        public static void SetFlashMode (this IntPtr camera, FlashMode state) {}
        public static void SetTorchEnabled (this IntPtr camera, bool state) {}
        public static void SetWhiteBalanceLock (this IntPtr camera, bool locked) {}
        public static void SetZoomRatio (this IntPtr camera, float ratio) {}
        #endif
    }
}
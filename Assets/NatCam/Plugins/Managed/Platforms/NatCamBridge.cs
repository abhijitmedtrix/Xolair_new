/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using System;
    using System.Runtime.InteropServices;

    public static partial class NatCamBridge {

        private const string Assembly =
        #if UNITY_IOS
        "__Internal";
        #else
        "NatCam";
        #endif

        #region ---Delegates---
        public delegate void StartCallback (IntPtr texPtr, int width, int height);
        public delegate void PreviewCallback (IntPtr texPtr, long timestamp);
        public delegate void PhotoCallback (IntPtr imgPtr, int width, int height);
        #endregion


        #if UNITY_IOS && !UNITY_EDITOR
        [DllImport(Assembly, EntryPoint = "NCCoreRegisterCallbacks")]
        public static extern void RegisterCoreCallbacks (StartCallback startCallback,  PreviewCallback previewCallback, PhotoCallback photoCallback, string context);
        [DllImport(Assembly, EntryPoint = "NCCoreCaptureFrame")]
        public static extern void CaptureFrame (byte[] ptr);
        [DllImport(Assembly, EntryPoint = "NCCoreCameraCount")]
        public static extern int CameraCount ();
        [DllImport(Assembly, EntryPoint = "NCCoreGetCamera")]
        public static extern int GetCamera ();
        [DllImport(Assembly, EntryPoint = "NCCoreIsRunning")]
        public static extern bool IsRunning ();
        [DllImport(Assembly, EntryPoint = "NCCoreStartPreview")]
        public static extern void StartPreview (IntPtr camera);
        [DllImport(Assembly, EntryPoint = "NCCoreStopPreview")]
        public static extern void StopPreview ();
        [DllImport(Assembly, EntryPoint = "NCCoreCapturePhoto")]
        public static extern void CapturePhoto ();
        [DllImport(Assembly, EntryPoint = "NCCoreOnOrient")]
        public static extern void OnOrient (int orientation);
        [DllImport(Assembly, EntryPoint = "NCCoreHasPermissions")]
        public static extern bool HasPermissions ();
        
        #else
        public static void RegisterCoreCallbacks (StartCallback startCallback,  PreviewCallback previewCallback, PhotoCallback photoCallback, string context) {}
        public static void CaptureFrame (byte[] ptr) {}
        public static int CameraCount () { return 0; }
        public static int GetCamera () { return -1; }
        public static bool IsRunning () { return false; }
        public static void StartPreview (IntPtr camera) {}
        public static void StopPreview () {}
        public static void CapturePhoto () {}
        public static void OnOrient (int orientation) {}
        public static bool HasPermissions () { return false; }
        #endif
    }
}
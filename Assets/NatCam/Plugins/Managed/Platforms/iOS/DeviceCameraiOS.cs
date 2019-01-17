/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using System;

    public class DeviceCameraiOS : DeviceCamera {

        #region --Getters--
        public override bool IsFrontFacing { get { return device.IsFrontFacing(); }}
        public override bool IsFlashSupported { get { return device.IsFlashSupported(); }}
        public override bool IsTorchSupported { get { return device.IsTorchSupported(); }}
        public override float HorizontalFOV { get { return device.HorizontalFOV(); }}
        public override float VerticalFOV { get { return device.VerticalFOV(); }}
        public override float MinExposureBias { get { return device.MinExposureBias(); }}
        public override float MaxExposureBias { get { return device.MaxExposureBias(); }}
        public override float MaxZoomRatio { get { return device.MaxZoomRatio(); }}
        #endregion


        #region --Properties--

        public override Vector2Int PreviewResolution {
            get {
                int width, height;
                device.PreviewResolution(out width, out height);
                return new Vector2Int(width, height);
            }
            set { device.SetPreviewResolution(value.x, value.y); }
        }

        public override Vector2Int PhotoResolution {
            get {
                int width, height;
                device.PhotoResolution(out width, out height);
                return new Vector2Int(width, height);
            }
            set { device.SetPhotoResolution(value.x, value.y); }
        }
        
        public override float Framerate {
            get { return device.Framerate(); }
            set { device.SetFramerate(value); }
        }

        public override bool FocusLock {
            get { return device.FocusLock(); }
            set { device.SetFocusLock(value); }
        }

        public override Vector2 FocusPoint {
            set { device.SetFocusPoint(value.x, value.y); }
        }

        public override bool ExposureLock {
            get { return device.ExposureLock(); }
            set { device.SetExposureLock(value); }
        }

        public override float ExposureBias {
            get { return device.ExposureBias(); }
            set { device.SetExposureBias(value); }
        }

        public override FlashMode FlashMode {
            get { return device.FlashMode(); } 
            set { device.SetFlashMode(value); }
        }

        public override bool TorchEnabled {
            get { return device.TorchEnabled(); } 
            set { device.SetTorchEnabled(value); }
        }

        public override bool WhiteBalanceLock {
            get { return device.WhiteBalanceLock(); }
            set { device.SetWhiteBalanceLock(value); }
        }

        public override float ZoomRatio {
            get { return device.ZoomRatio(); } 
            set { device.SetZoomRatio(value); }
        }
        #endregion


        #region --Operations--

        private readonly IntPtr device;

        public DeviceCameraiOS (int index) : base(index) {
            device = DeviceBridge.AcquireCamera(index);
        }

        public static explicit operator IntPtr (DeviceCameraiOS camera) {
            return camera.device;
        }
        #endregion
    }
}
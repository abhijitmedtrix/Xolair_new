/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;

    public class DeviceCameraLegacy : DeviceCamera {

        #region --Getters--
        public override bool IsFrontFacing {
            get {
                return Device.isFrontFacing;
            }
        }

        public override bool IsFlashSupported {
            get {
                Debug.LogWarning("NatCam Error: Flash is not supported on legacy backend");
                return false;
            }
        }

        public override bool IsTorchSupported {
            get {
                Debug.LogWarning("NatCam Error: Torch is not supported on legacy backend");
                return false;
            }
        }

        public override float HorizontalFOV {
            get {
                Debug.LogWarning("NatCam Error: Field of view is not supported on legacy backend");
                return 0f;
            }
        }

        public override float VerticalFOV {
            get {
                Debug.LogWarning("NatCam Error: Field of view is not supported on legacy backend");
                return 0f;
            }
        }

        public override float MinExposureBias {
            get {
                Debug.LogWarning("NatCam Error: Exposure is not supported on legacy backend");
                return 0f;
            }
        }

        public override float MaxExposureBias {
            get {
                Debug.LogWarning("NatCam Error: Exposure is not supported on legacy backend");
                return 0f;
            }
        }

        public override float MaxZoomRatio {
            get {
                Debug.LogWarning("NatCam Error: Zoom is not supported on legacy backend");
                return 1f;
            }
        }
        #endregion


        #region --Properties--

        public override Vector2Int PreviewResolution { get; set; }

        public override Vector2Int PhotoResolution {
            get {
                Debug.LogWarning("NatCam Error: Photo resolution is not supported on legacy backend");
                return PreviewResolution;
            }
            set {
                Debug.LogWarning("NatCam Error: Photo resolution is not supported on legacy backend");
            }
        }
        public override float Framerate { get; set; }

        public override bool FocusLock {
            get {
                Debug.LogWarning("NatCam Error: Focus mode is not supported on legacy backend");
                return false;
            } 
            set {
                Debug.LogWarning("NatCam Error: Focus mode is not supported on legacy backend");
            }
        }

        public override Vector2 FocusPoint {
            set {
                Debug.LogWarning("NatCam Error: Focus is not supported on legacy backend");
            }
        }

        public override bool ExposureLock {
            get {
                Debug.LogWarning("NatCam Error: Exposure mode is not supported on legacy backend");
                return false;
            } 
            set {
                Debug.LogWarning("NatCam Error: Exposure mode is not supported on legacy backend");
            }
        }

        public override float ExposureBias {
            get {
                Debug.LogWarning("NatCam Error: Exposure is not supported on legacy backend");
                return 0f;
            } 
            set {
                Debug.LogWarning("NatCam Error: Exposure is not supported on legacy backend");
            }
        }

        public override FlashMode FlashMode {
            get {
                Debug.LogWarning("NatCam Error: Flash mode is not supported on legacy backend");
                return 0;
            } 
            set {
                Debug.LogWarning("NatCam Error: Flash mode is not supported on legacy backend");
            }
        }

        public override bool TorchEnabled {
            get {
                Debug.LogWarning("NatCam Error: Torch is not supported on legacy backend");
                return false;
            } 
            set {
                Debug.LogWarning("NatCam Error: Torch is not supported on legacy backend");
            }
        }

        public override bool WhiteBalanceLock {
            get {
                Debug.LogWarning("NatCam Error: White balance is not supported on legacy backend");
                return false;
            } 
            set {
                Debug.LogWarning("NatCam Error: White balance is not supported on legacy backend");
            }
        }

        public override float ZoomRatio {
            get {
                Debug.LogWarning("NatCam Error: Zoom is not supported on legacy backend");
                return 1f;
            } 
            set {
                Debug.LogWarning("NatCam Error: Zoom is not supported on legacy backend");
            }
        }
        #endregion
        

        #region --Operations--

        public readonly WebCamDevice Device;

        public DeviceCameraLegacy (int index) : base(index) {
            Device = WebCamTexture.devices[index];
        }
        #endregion
    }
}
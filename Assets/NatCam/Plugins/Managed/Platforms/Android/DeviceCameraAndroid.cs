/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;

    public class DeviceCameraAndroid : DeviceCamera {

        #region --Getters--
        public override bool IsFrontFacing { get { return device.Call<bool>("isFrontFacing"); }}
        public override bool IsFlashSupported { get { return device.Call<bool>("isFlashSupported"); }}
        public override bool IsTorchSupported { get { return device.Call<bool>("isTorchSupported"); }}
        public override float HorizontalFOV { get { return device.Call<float>("horizontalFOV"); }}
        public override float VerticalFOV { get { return device.Call<float>("verticalFOV"); }}
        public override float MinExposureBias { get { return device.Call<float>("minExposureBias"); }}
        public override float MaxExposureBias { get { return device.Call<float>("maxExposureBias"); }}
        public override float MaxZoomRatio { get { return device.Call<float>("maxZoomRatio"); }}
        #endregion


        #region --Properties--

        public override Vector2Int PreviewResolution {
            get {
                AndroidJavaObject jRet = device.Call<AndroidJavaObject>("previewResolution");
                if (jRet.GetRawObject().ToInt32() == 0)
                    return default(Vector2Int);
                int[] res = AndroidJNIHelper.ConvertFromJNIArray<int[]>(jRet.GetRawObject());
                var resolution = new Vector2Int(res[0], res[1]);
                jRet.Dispose();
                return resolution;
            }
            set { device.Call("setPreviewResolution", value.x, value.y); }
        }

        public override Vector2Int PhotoResolution {
            get {
                AndroidJavaObject jRet = device.Call<AndroidJavaObject>("photoResolution");
                if (jRet.GetRawObject().ToInt32() == 0)
                    return default(Vector2Int);
                int[] res = AndroidJNIHelper.ConvertFromJNIArray<int[]>(jRet.GetRawObject());
                var resolution = new Vector2Int(res[0], res[1]);
                jRet.Dispose();
                return resolution;
            }
            set { device.Call("setPhotoResolution", value.x, value.y); }
        }
        
        public override float Framerate {
            get { return device.Call<float>("framerate"); }
            set { device.Call("setFramerate", value); }
        }

        public override bool FocusLock {
            get { return device.Call<bool>("focusLock"); }
            set { device.Call("setFocusLock", value); }
        }

        public override Vector2 FocusPoint {
            set { device.Call("setFocusPoint", value.x, value.y); }
        }

        public override bool ExposureLock {
            get { return device.Call<bool>("exposureLock"); }
            set { device.Call("setExposureLock", value); }
        }

        public override float ExposureBias {
            get { return device.Call<float>("exposureBias"); } 
            set { device.Call("setExposureBias", (int)value); }
        }

        public override FlashMode FlashMode {
            get { return (FlashMode)device.Call<int>("flashMode"); } 
            set { device.Call("setFlashMode", (int)value); }
        }

        public override bool TorchEnabled {
            get { return device.Call<bool>("torchEnabled"); } 
            set { device.Call("setTorchEnabled", value); }
        }

        public override bool WhiteBalanceLock {
            get { return device.Call<bool>("whiteBalanceLock"); }
            set { device.Call("setWhiteBalanceLock", value); }
        }

        public override float ZoomRatio {
            get { return device.Call<float>("zoomRatio"); } 
            set { device.Call("setZoomRatio", value); }
        }
        #endregion


        #region --Operations--

        private readonly AndroidJavaObject device;

        public DeviceCameraAndroid (int index) : base(index) {
            device = new AndroidJavaObject("com.yusufolokoba.natcam.DeviceCamera", index);
        }

        public static explicit operator AndroidJavaObject (DeviceCameraAndroid camera) {
            return camera.device;
        }
        #endregion
    }
}
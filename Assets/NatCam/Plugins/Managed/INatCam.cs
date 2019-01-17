/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core.Platforms {

    using UnityEngine;
    using System;

    public interface INatCam {

        #region --Properties--
        bool HasPermissions { get; }
        int CameraCount { get; }
        DeviceCamera Camera { get; }
        Texture Preview { get; }
        bool IsRunning { get; }
        #endregion
        
        #region --Operations--
        void StartPreview (DeviceCamera camera, Action startCallback, Action frameCallback);
        void StopPreview ();
        void CapturePhoto (Action<Texture2D> callback);
        void CaptureFrame (byte[] dest);
        #endregion
    }
}
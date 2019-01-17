/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatCamU.Core {

	using UnityEngine;
	using System;
    using Docs;

    #region --Enumerations--
    /// <summary>
    /// Camera flash mode
    /// </summary>
    [Doc(@"FlashMode")]
    public enum FlashMode {
		[Doc(@"FlashOff")] Off = 0,
        [Doc(@"FlashOn")] On = 1,
        [Doc(@"FlashAuto")] Auto = 2
	}
    #endregion


    #region --Value Types--
    #if !UNITY_2017_1_OR_NEWER
    public struct Vector2Int {
        public int x, y;
        public Vector2Int (int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    #endif
    #endregion
}
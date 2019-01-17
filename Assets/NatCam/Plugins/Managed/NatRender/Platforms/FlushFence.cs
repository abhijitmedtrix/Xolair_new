/* 
*   NatRender
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatRenderU.Platforms {

    using UnityEngine;
    using System;

    public class FlushFence : GPUFence {

        #region --Client API--

        public FlushFence (SynchronizationHint synchronizationHint) : base(synchronizationHint) {}

        public override void WaitForCompletion (Action completionHandler) {
            GL.Flush();
            completionHandler();
        }
        #endregion
    }
}
/* 
*   NatRender
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatRenderU.Platforms {

    using AOT;
    using UnityEngine;
    using System;
    using System.Runtime.InteropServices;

    public class MTLFence : GPUFence {

        #region --Op vars--
        private volatile Action completionHandler;
        #endregion


        #region --GPUFence--

        public MTLFence (SynchronizationHint synchronizationHint) : base(synchronizationHint) {
            var weakSelf = (IntPtr)GCHandle.Alloc(this, GCHandleType.Weak);
            GPUFenceSync(synchronizationHint, OnSync, weakSelf);
        }

        public override void WaitForCompletion (Action completionHandler) {
            this.completionHandler = completionHandler;
        }
        #endregion


        #region --Operations--

        private delegate void SyncDelegate (IntPtr context);

        #if UNITY_IOS
        [DllImport("__Internal", EntryPoint = "NRGPUFenceSync")]
        #else
        [DllImport("NatRender", EntryPoint = "NRGPUFenceSync")]
        #endif
        private static extern void GPUFenceSync (SynchronizationHint syncHint, SyncDelegate handler, IntPtr context);

        [MonoPInvokeCallback(typeof(SyncDelegate))]
        private static void OnSync (IntPtr fenceHandle) { // CHECK // Thread
            GCHandle handle = (GCHandle)(IntPtr)fenceHandle;
            MTLFence target = handle.Target as MTLFence;
            if (target == null)
                return;
            target.completionHandler();
        }
        #endregion
    }
}
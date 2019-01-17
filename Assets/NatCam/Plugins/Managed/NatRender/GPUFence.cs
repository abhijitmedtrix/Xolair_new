/* 
*   NatRender
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatRenderU {

    using UnityEngine;
    using System;
    using Platforms;

    public abstract class GPUFence {

        #region --Ops--
        protected readonly SynchronizationHint synchronizationHint;

        protected GPUFence (SynchronizationHint synchronizationHint) {
            this.synchronizationHint = synchronizationHint;
        }
        #endregion


        #region --Client API--

        /// <summary>
        /// Create and insert a fence into the GPU pipeline
        /// </summary>
        public static GPUFence Create (SynchronizationHint synchronizationHint) {
            #if UNITY_EDITOR
            return new FlushFence(synchronizationHint);
            #elif UNITY_ANDROID
            return new GLESFence(synchronizationHint);
            #elif UNITY_IOS
            return new MTLFence(synchronizationHint); // We don't support GLES on iOS
            #else
            return new FlushFence(synchronizationHint);
            #endif
        }

        /// <summary>
        /// Register a callback to be invoked when all graphics commands up to this point have been completed.
        /// Note that the handler may be invoked on any arbitrary thread.
        /// So do not rely on the callback being invoked on any particular thread (especially not the Unity main thread).
        /// </summary>
        public abstract void WaitForCompletion (Action completionHandler);
        #endregion
    }
}
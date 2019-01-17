/* 
*   NatRender
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatRenderU.Platforms {

    using UnityEngine;
    using System;
    using Dispatch;

    public class GLESFence : GPUFence {

        #region --Op vars--
        private volatile Action completionHandler;
        private readonly Dispatcher dispatcher;
        private readonly AndroidJavaObject fence;
        #endregion


        #region --GPUFence--

        public GLESFence (SynchronizationHint hint) : base(hint) {
            dispatcher = Dispatcher.Create(DispatchThread.RenderThread);
            fence = new AndroidJavaObject("com.yusufolokoba.natrender.GPUFence", (int)hint);
            DispatchUtility.onFrame += CheckForCompletion;
        }

        public override void WaitForCompletion (Action completionHandler) {
            this.completionHandler = completionHandler;
        }
        #endregion


        #region --Operations--

        private void CheckForCompletion () {
            dispatcher.Dispatch(() => {
                if (fence.Call<bool>("complete")) {
                    // Invoke completion handler
                    if (completionHandler != null)
                        completionHandler();
                    completionHandler = null;
                    // Dispose
                    DispatchUtility.onFrame -= CheckForCompletion;
                    fence.Call("dispose");
                    dispatcher.Dispose();
                }
            });
        }
        #endregion
    }
}
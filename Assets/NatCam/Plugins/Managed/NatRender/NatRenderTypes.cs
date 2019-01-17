/* 
*   NatRender
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatRenderU {

    public enum DispatchThread {
        /// <summary>
        /// Dispatched workloads will be invoked on the Unity main thread
        /// </summary>
        MainThread,
        /// <summary>
        /// Dispatched workloads will be invoked on a worker thread
        /// </summary>
        WorkerThread,
        /// <summary>
        /// Dispatched workloads will be invoked on the Unity render thread.
        /// </summary>
        RenderThread
    }

    public enum SynchronizationHint {
        /// <summary>
        /// GPU fence will take very little frame time (often 0ms) at the expense of memory.
        /// This mode is highly recommended to keep the game running fluidly.
        /// </summary>
        LessFrameTime = 1,
        /// <summary>
        /// GPU fence will try to enforce synchronize immediately, at the expense of CPU time.
        /// This mode is useful on memory-constrained environments.
        /// </summary>
        LessMemory = 2
    }
}
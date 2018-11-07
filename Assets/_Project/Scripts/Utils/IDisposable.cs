/// <summary>
/// Disposable interface used in ObjectPool to define, can object be disposed on Destroyong (pulling back to pool) or not.
/// </summary>
public interface IDisposable {
	void Dispose();
}

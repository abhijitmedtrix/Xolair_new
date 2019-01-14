using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    private static List<Texture2D> _snapshots = new List<Texture2D>();
    private static WebCamTexture _webCamTexture;

    public static event Action OnCameraStart;
    public static event Action<List<Texture2D>> OnCameraComplete;

    public static void StartCamera(RawImage image)
    {
        if (_webCamTexture == null)
        {
            _webCamTexture = new WebCamTexture();
        }

        _webCamTexture.Play();
        image.texture = _webCamTexture;

        _snapshots.Clear();

        OnCameraStart?.Invoke();
    }

    public static Texture2D TakePhoto()
    {
        Texture2D snapshot = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        snapshot.SetPixels(_webCamTexture.GetPixels());
        snapshot.Apply();

        _snapshots.Add(snapshot);

        return snapshot;
    }

    public static void RemoveLastSnapshot()
    {
        if (_snapshots.Count > 0)
        {
            Texture2D texture = _snapshots[_snapshots.Count - 1];
            _snapshots.Remove(texture);
            GameObject.Destroy(texture);
        }
    }

    public static void StopCamera()
    {
        _webCamTexture.Stop();
        
        // clear the memory
        Resources.UnloadUnusedAssets();
     
        OnCameraComplete?.Invoke(_snapshots);
    }
}
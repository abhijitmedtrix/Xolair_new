using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    private static List<Texture2D> _snapshots = new List<Texture2D>();

    public static WebCamTexture webCamTexture;

    public static event Action OnCameraStart;
    public static event Action<List<Texture2D>> OnCameraComplete;

    public static void StartCamera(RawImage image)
    {
        if (webCamTexture == null)
        {
            webCamTexture = new WebCamTexture(Screen.width, Screen.height);
        }

        webCamTexture.Play();
        image.texture = webCamTexture;

        _snapshots.Clear();

        OnCameraStart?.Invoke();
    }

    public static Texture2D TakePhoto()
    {
        Texture2D snapshot = new Texture2D(webCamTexture.width, webCamTexture.height);
        // Texture2D snapshot = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        snapshot.SetPixels(webCamTexture.GetPixels());
        Debug.Log(
            $"Photo taken. Width: {webCamTexture.width}, Height: {webCamTexture.height}, req w: {webCamTexture.requestedWidth}, req h: {webCamTexture.requestedHeight}, rotated? {webCamTexture.videoRotationAngle}, flipped? {webCamTexture.videoVerticallyMirrored}");

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
        webCamTexture.Stop();

        // clear the memory
        Resources.UnloadUnusedAssets();

        OnCameraComplete?.Invoke(_snapshots);
    }
}
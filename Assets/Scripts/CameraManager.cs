using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private static List<Texture2D> _snapshots = new List<Texture2D>();
    public static WebCamTexture webCamTexture;
    public static event Action OnCameraStart;
    public static event Action<List<Texture2D>> OnCameraComplete;

    public static void StartCamera(RawImage image)
    {
        if (webCamTexture == null)
        {
            // webCamTexture = new WebCamTexture(Screen.width, Screen.height, 60);
            webCamTexture = new WebCamTexture();
        }

        webCamTexture.Play();
        image.texture = webCamTexture;

        Debug.Log(
            $"Web texture initialized. Width: {webCamTexture.width}, Height: {webCamTexture.height}, req w: {webCamTexture.requestedWidth}, req h: {webCamTexture.requestedHeight}, rotated? {webCamTexture.videoRotationAngle}, flipped? {webCamTexture.videoVerticallyMirrored}");

        _snapshots.Clear();

        OnCameraStart?.Invoke();
    }

    public static Texture2D TakePhoto()
    {
        // Texture2D snapshotTexture = new Texture2D(webCamTexture.requestedWidth, webCamTexture.requestedHeight,
        Texture2D snapshotTexture = new Texture2D(webCamTexture.width, webCamTexture.height,
            TextureFormat.RGB24, false);
        snapshotTexture.anisoLevel = 9;

        // Camera cam = Camera.main;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        // read pixels will read from the currently active render texture so make our offscreen 
        // render texture active and then read the pixels
        snapshotTexture.ReadPixels(rect, 0, 0);
        snapshotTexture.Apply();

        _snapshots.Add(snapshotTexture);

        return snapshotTexture;
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
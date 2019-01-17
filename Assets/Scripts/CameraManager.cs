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
        // if (_webCamTexture == null)
        // {
        //     _webCamTexture = new WebCamTexture(Screen.width, Screen.height);
        // }

        // NativeCamera.TakePicture(new NativeCamera.CameraCallback());
        
        
        // _webCamTexture.Play();
        // image.texture = _webCamTexture;
        //
        // _snapshots.Clear();

        OnCameraStart?.Invoke();
    }

    public static Texture2D TakePhoto()
    {
        Texture2D snapshot = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        // Texture2D snapshot = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        snapshot.SetPixels(_webCamTexture.GetPixels());
        Debug.Log($"Photo taken. Width: {_webCamTexture.width}, Height: {_webCamTexture.height}, req w: {_webCamTexture.requestedWidth}, req h: {_webCamTexture.requestedHeight}, rotated? {_webCamTexture.videoRotationAngle}, flipped? {_webCamTexture.videoVerticallyMirrored}");
        
        /*
        float physical = (float)_webCamTexture.width/(float)_webCamTexture.height;
        rawImageARF.aspectRatio = physical;

        float scaleY = wct.videoVerticallyMirrored ? -1f : 1f;
        rawImageRT.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -wct.videoRotationAngle;
        rawImageRT.localEulerAngles = new Vector3(0f,0f,orient);
        */
        
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
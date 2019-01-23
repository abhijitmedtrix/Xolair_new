using System;
using System.Collections.Generic;
using NatCamU.Core;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private static List<Texture2D> _snapshots = new List<Texture2D>();

    public static event Action<Texture> OnCameraStart;
    public static event Action<Texture2D> OnPhotoTaken;
    public static event Action<List<Texture2D>> OnCameraComplete;

    public static void StartCamera()
    {
        DeviceCamera camera = DeviceCamera.RearCamera;
        if (camera == null)
        {
            camera = DeviceCamera.FrontCamera;
            if (camera == null)
            {
                Debug.LogWarning("No device camera found.");
                return;
            }
        }
        
        Vector2Int newSize = new Vector2Int(Mathf.FloorToInt(Screen.width / 2f), Mathf.FloorToInt(Screen.height / 2f));
        camera.PhotoResolution = camera.PreviewResolution = newSize;
        camera.Framerate = 30;
        Debug.Log("New PhotoResolution size: "+newSize);
        
        NatCam.StartPreview(camera, OnStart);

        for (int i = 0; i < _snapshots.Count; i++)
        {
            if (_snapshots[i] != null)
            {
                Destroy(_snapshots[i]);
            }
        }
        _snapshots.Clear();
    }
    
    public static void StopCamera()
    {
        NatCam.StopPreview();
        
        // clear the memory
        Resources.UnloadUnusedAssets();

        OnCameraComplete?.Invoke(_snapshots);
    }
    
    
    
    public static void TakePhoto()
    {
        NatCam.CapturePhoto(OnPhoto);
    }

    private static void OnStart()
    {
        // Set flash to auto
        NatCam.Camera.FlashMode = FlashMode.Auto;
        
        Debug.Log($"NatCam.Preview resolution w: {NatCam.Preview.width}, h: {NatCam.Preview.height}");
        Debug.Log($"Cam params. dimension: {NatCam.Preview.dimension}, anisoLevel: {NatCam.Preview.anisoLevel}, w: {NatCam.Preview.width}, h: {NatCam.Preview.height}");
        
        Vector2Int newSize = new Vector2Int(Mathf.FloorToInt(Screen.width / 2f), Mathf.FloorToInt(Screen.height / 2f));
        NatCam.Camera.PreviewResolution = newSize;
        
        Debug.Log($"After Cam params. dimension: {NatCam.Preview.dimension}, anisoLevel: {NatCam.Preview.anisoLevel}, w: {NatCam.Preview.width}, h: {NatCam.Preview.height}");
        
        OnCameraStart?.Invoke(NatCam.Preview);
    }
    
    private static void OnPhoto(Texture2D photo)
    {
        Debug.Log($"Photo resolution w: {photo.width}, h: {photo.height}");
        
        // Cache the photo
        _snapshots.Add(photo);
        
        OnPhotoTaken?.Invoke(photo);
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
}
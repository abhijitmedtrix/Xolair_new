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
        
        OnCameraStart?.Invoke(NatCam.Preview);
    }
    
    private static void OnPhoto(Texture2D photo)
    {
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
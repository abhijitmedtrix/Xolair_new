using System;
using System.Collections.Generic;
using System.IO;
using App.Utils;
using NatCamU.Core;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private static int _savedCounter;
    private static List<Texture2D> _snapshots = new List<Texture2D>();

    public static event Action<Texture> OnCameraStart;
    public static event Action<Texture2D> OnPhotoTaken;
    public static event Action<List<Texture2D>> OnCameraComplete;

    private const string _TEMP_PHOTO_PATH = "temp/photos";

    public static void StartCamera()
    {
        _savedCounter = 0;
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
        Debug.Log("New PhotoResolution size: " + newSize);

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
        Debug.Log(
            $"Cam params. dimension: {NatCam.Preview.dimension}, anisoLevel: {NatCam.Preview.anisoLevel}, w: {NatCam.Preview.width}, h: {NatCam.Preview.height}");

        // Vector2Int newSize = new Vector2Int(768, 1366);
        // NatCam.Camera.PreviewResolution = newSize;
        // NatCam.Camera.PhotoResolution = newSize;

        // Debug.Log(
        // $"After Cam params. dimension: {NatCam.Preview.dimension}, anisoLevel: {NatCam.Preview.anisoLevel}, w: {NatCam.Preview.width}, h: {NatCam.Preview.height}");

        OnCameraStart?.Invoke(NatCam.Preview);
        
        // remove temp folder if exists
        string dirPath = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH);
        DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        if (dirInfo.Exists)
        {
            dirInfo.Delete(true);
        }
        
        dirInfo.Create();
    }

    private static void OnPhoto(Texture2D photo)
    {
        Debug.Log($"Photo resolution w: {photo.width}, h: {photo.height}");

        // Cache the photo
        _snapshots.Add(photo);

        OnPhotoTaken?.Invoke(photo);
    }

    public static void SaveLastPhoto()
    {
        Texture2D photo = _snapshots[_snapshots.Count - 1];

        string filePath = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH, $"{_savedCounter}.png");

        File.WriteAllBytes(filePath, photo.EncodeToPNG());
        TextureScale.Bilinear(photo, photo.width / 4, photo.height / 4);
        
        _savedCounter++;
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

    public static List<string> GetTexturesPaths(List<int> indexes, bool clearVariables)
    {
        List<string> paths = new List<string>();

        for (int i = 0; i < _snapshots.Count; i++)
        {
            string path = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH, $"{i}.png");

            // if it's a selected photo
            if (indexes.Contains(i))
            {
                paths.Add(path);
            }
            else
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else
                {
                    Debug.LogError($"There is no photo to delete by path: {path}");
                }
            }
        }

        if (clearVariables)
        {
            _snapshots.Clear();
            _savedCounter = 0;
        }

        return paths;
    }
}
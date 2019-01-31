using System;
using System.Collections.Generic;
using System.IO;
using App.Utils;
using NatCamU.Core;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private static int _savedCounter;
    private static Texture2D _cachedPhoto;
    private static List<Texture2D> _previews = new List<Texture2D>();
    
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

        Clear();
    }

    private static void Clear()
    {
        if (_cachedPhoto != null)
        {
            Destroy(_cachedPhoto);
        }

        _cachedPhoto = null;

        for (int i = 0; i < _previews.Count; i++)
        {
            if (_previews[i] != null)
            {
                Destroy(_previews[i]);
            }
        }
        _previews.Clear();
    }

    public static void StopCamera()
    {
        NatCam.StopPreview();

        // clear the memory
        Resources.UnloadUnusedAssets();

        OnCameraComplete?.Invoke(_previews);
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

        float screenAspect = (float) Screen.width / Screen.height;
        float cameraAspect = (float) photo.width / photo.height;

        Rect rect;

        // if screen is wider then camera
        if (screenAspect > cameraAspect)
        {
            float diff = photo.height - photo.width / cameraAspect;
            rect = new Rect(0, diff / 2, photo.width, photo.height - diff);
        }
        else
        {
            float diff = photo.width - photo.height * screenAspect;
            rect = new Rect(diff / 2, 0, photo.width - diff, photo.height);
        }

        // crop texture to fit the screen dimension
        Color[] c = photo.GetPixels((int) rect.x, (int) rect.y, (int) rect.width, (int) rect.height);

        photo.Resize((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
        photo.SetPixels(c);
        photo.Apply();
        c = null;

        _cachedPhoto = photo;
        
        OnPhotoTaken?.Invoke(photo);
    }

    public static void SaveLastPhoto()
    {
        string filePath = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH, $"{_savedCounter}.png");
        byte[] bytes = _cachedPhoto.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
        bytes = null;

        // Debug.Log($"Photo texture format: {photo.format}");
        TextureScale.Bilinear(_cachedPhoto, _cachedPhoto.width / 4, _cachedPhoto.height / 4);
        Debug.Log("Resized texture");
        // Destroy(_cachedPhoto);
        // _cachedPhoto = null;
        _previews.Add(_cachedPhoto);

        _savedCounter++;

        Resources.UnloadUnusedAssets();
    }

    public static void RemoveLastSnapshot()
    {
        if (_cachedPhoto != null)
        {
            Destroy(_cachedPhoto);
            _cachedPhoto = null;
        }
    }

    public static List<string> GetTexturesPaths(List<int> indexes, bool clearVariables)
    {
        List<string> paths = new List<string>();

        for (int i = 0; i < _savedCounter; i++)
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
            Clear();
            _savedCounter = 0;
        }

        return paths;
    }
}
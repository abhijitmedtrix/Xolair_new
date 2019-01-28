using System.Collections;
using System.Collections.Generic;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _takePhotoContent, _managePhotoContent;
    [SerializeField] protected RawImage _cameraRawImage;
    [SerializeField] protected RawImage _snapshotImage;
    [SerializeField] protected AudioClip _shutterClip;
    [SerializeField] protected AspectRatioFitter _aspectRatioFitter;
    [SerializeField] protected GameObject[] _objectsToHideForPhoto;
    
    public void StartCamera()
    {
        _takePhotoContent.SetActive(true);
        _managePhotoContent.SetActive(false);
        _snapshotImage.gameObject.SetActive(false);

        CameraManager.OnCameraStart += OnCameraStart;
        CameraManager.OnPhotoTaken += OnPhotoTaken;
        CameraManager.OnCameraComplete += CameraManagerOnCameraComplete;
        CameraManager.StartCamera();
    }

    public void TakePhoto()
    {
        // don't need anymore because NatCam play native shutter sound
        // AudioManager.Instance.Play(_shutterClip);

        _takePhotoContent.SetActive(false);
        _managePhotoContent.SetActive(true);

        // set snapshot texture
        _snapshotImage.gameObject.SetActive(true);

        // show only raw image to read screen pixels
        for (int i = 0; i < _objectsToHideForPhoto.Length; i++)
        {
            _objectsToHideForPhoto[i].SetActive(false);
        }
        
        CameraManager.TakePhoto();
    }

    private void OnCameraStart(Texture texture)
    {
        _cameraRawImage.texture = texture;
        _aspectRatioFitter.aspectRatio = (float)texture.width / texture.height;
    }

    private void OnPhotoTaken(Texture2D photo)
    {
        _snapshotImage.texture = photo;
        
        // enable objects
        for (int i = 0; i < _objectsToHideForPhoto.Length; i++)
        {
            _objectsToHideForPhoto[i].SetActive(true);
        }
    }
    
    private void CameraManagerOnCameraComplete(List<Texture2D> textures)
    {
    }

    public void SavePhoto()
    {
        CameraManager.SaveLastPhoto();
        
        _snapshotImage.gameObject.SetActive(false);

        _takePhotoContent.SetActive(true);
        _managePhotoContent.SetActive(false);
    }

    public void RemovePhoto()
    {
        CameraManager.RemoveLastSnapshot();
        
        _snapshotImage.gameObject.SetActive(false);

        _takePhotoContent.SetActive(true);
        _managePhotoContent.SetActive(false);
    }

    public void Finish()
    {
        CameraManager.OnCameraStart -= OnCameraStart;
        CameraManager.OnPhotoTaken -= OnPhotoTaken;
        CameraManager.OnCameraComplete -= CameraManagerOnCameraComplete;
        
        _snapshotImage.gameObject.SetActive(false);

        // show previous screen
        ScreenManager.Instance.Back();

        CameraManager.StopCamera();
    }
}
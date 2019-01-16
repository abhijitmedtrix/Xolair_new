using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _takePhotoContent, _managePhotoContent;
    [SerializeField] protected RawImage _rawImage;
    [SerializeField] protected RawImage _snapshotImage;
    [SerializeField] protected AudioClip _shutterClip;

    public void StartCamera()
    {
        CameraManager.StartCamera(_rawImage);   
        
        _takePhotoContent.SetActive(true);
        _managePhotoContent.SetActive(false);
        _snapshotImage.gameObject.SetActive(false);
    }
    
    public void TakePhoto()
    {
        AudioManager.Instance.Play(_shutterClip);

        _takePhotoContent.SetActive(false);
        _managePhotoContent.SetActive(true);

        // set snapshot texture
        _snapshotImage.gameObject.SetActive(true);
        _snapshotImage.texture = CameraManager.TakePhoto();
    }

    public void SavePhoto()
    {
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
        CameraManager.StopCamera();
        _snapshotImage.gameObject.SetActive(false);
        
        ScreenManager.Instance.Back();
    }
}
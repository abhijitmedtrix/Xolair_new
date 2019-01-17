using System.Collections;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class CameraScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _takePhotoContent, _managePhotoContent;
    [SerializeField] protected RawImage _cameraRawImage;
    [SerializeField] protected RawImage _snapshotImage;
    [SerializeField] protected AudioClip _shutterClip;
    [SerializeField] protected RectTransform _rawImageRT;
    [SerializeField] protected RectTransform _parentRT;
    [SerializeField] protected GameObject[] _objectsToHideForPhoto;
    
    public void StartCamera()
    {
        CameraManager.StartCamera(_cameraRawImage);

        // change as user rotates iPhone or Android:
        WebCamTexture wct = CameraManager.webCamTexture;

        // implementation by
        // https://answers.unity.com/questions/773464/webcamtexture-correct-resolution-and-ratio.html?childToView=1148424#answer-1148424

        int cwNeeded = wct.videoRotationAngle;
        bool willBeRotated = cwNeeded != 0;
        
        // Unity helpfully returns the _clockwise_ twist needed
        // guess nobody at Unity noticed their product works in counterclockwise:
        int ccwNeeded = -cwNeeded;

        // IF the image needs to be mirrored, it seems that it
        // ALSO needs to be spun. Strange: but true.
        if (wct.videoVerticallyMirrored) ccwNeeded += 180;

        // you'll be using a UI RawImage, so simply spin the RectTransform
        _rawImageRT.localEulerAngles = new Vector3(0f, 0f, ccwNeeded);

        float videoRatio = (float) wct.width / (float) wct.height;
        Debug.Log($"W: {wct.width}, H: {wct.height}, cwNeeded: {cwNeeded}, willBeRotated: {willBeRotated}, videoRatio: {videoRatio}, mirrored?: {wct.videoVerticallyMirrored}");

        if (willBeRotated)
        {
            _rawImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _parentRT.rect.height);
            _rawImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _parentRT.rect.width);
        }
        else
        {
            _rawImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _parentRT.rect.width);
            _rawImageRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _parentRT.rect.height);
        }
        
        // alert, the ONLY way to mirror a RAW image, is, the uvRect.
        // changing the scale is completely broken.
        if (wct.videoVerticallyMirrored)
        {
            _cameraRawImage.uvRect = new Rect(1, 0, -1, 1); // means flip on vertical axis
        }
        else
        {
            _cameraRawImage.uvRect = new Rect(0, 0, 1, 1); // means no flip
        }

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

        StartCoroutine(TakeSnapshot());
        // _snapshotImage.texture = CameraManager.TakePhoto();
    }

    private IEnumerator TakeSnapshot()
    {
        // show only raw image to read screen pixels
        for (int i = 0; i < _objectsToHideForPhoto.Length; i++)
        {
            _objectsToHideForPhoto[i].SetActive(false);
        }
        
        yield return new WaitForEndOfFrame();

        _snapshotImage.texture = CameraManager.TakePhoto();
        
        // enable objects
        for (int i = 0; i < _objectsToHideForPhoto.Length; i++)
        {
            _objectsToHideForPhoto[i].SetActive(true);
        }
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
        _snapshotImage.gameObject.SetActive(false);

        // show previous screen
        ScreenManager.Instance.Back();

        CameraManager.StopCamera();
    }
}
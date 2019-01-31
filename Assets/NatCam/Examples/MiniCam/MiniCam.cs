/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

using System.Collections.Generic;
using System.IO;
using App.Utils;

namespace NatCamU.Examples
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using Core;

    public class MiniCam : MonoBehaviour
    {
        [Header("Camera")] public bool useFrontCamera;

        [Header("UI")] public RawImage rawImage;
        public AspectRatioFitter aspectFitter;
        public Text flashText;
        public Button switchCamButton, flashButton;
        public Image checkIco, flashIco;
        private Texture2D photo;


        #region --Unity Messages--

        // Use this for initialization
        private void Start()
        {
            // Start camera preview
            var camera = useFrontCamera ? DeviceCamera.FrontCamera : DeviceCamera.RearCamera;
            if (!camera)
            {
                Debug.LogWarning("Camera is null. Consider using " + (useFrontCamera ? "rear" : "front") + " camera");
                camera = useFrontCamera ? DeviceCamera.RearCamera : DeviceCamera.FrontCamera;
                // return;
            }

            NatCam.StartPreview(camera, OnStart);
        }

        #endregion


        #region --Callbacks--

        private void OnStart()
        {
            // Display the preview
            rawImage.texture = NatCam.Preview;
            aspectFitter.aspectRatio = NatCam.Preview.width / (float) NatCam.Preview.height;

            // Set flash to auto
            NatCam.Camera.FlashMode = FlashMode.Auto;

            NatCam.Camera.PhotoResolution = NatCam.Camera.PreviewResolution = new Vector2Int(720, 1280);

            SetFlashIcon();
            
            // remove temp folder if exists
            string dirPath = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH);
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
            dirInfo.Create();
        }

        private void OnPhoto(Texture2D photo)
        {
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
            Color[] c = photo.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
            // Texture2D cropped = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
            photo.Resize((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
            photo.SetPixels(c);
            photo.Apply();
            c = null;
            // Destroy(photo);
            // photo = cropped;

            
            
            // Cache the photo
            this.photo = photo;
            // Display the photo
            rawImage.texture = photo;
            // Scale the panel to match aspect ratios
            aspectFitter.aspectRatio = photo.width / (float) photo.height;
            // Enable the check icon
            checkIco.gameObject.SetActive(true);
            // Disable the switch camera button
            switchCamButton.gameObject.SetActive(false);
            // Disable the flash button
            flashButton.gameObject.SetActive(false);
        }

        private void OnView()
        {
            // Disable the check icon
            checkIco.gameObject.SetActive(false);
            // Display the preview
            rawImage.texture = NatCam.Preview;
            // Scale the panel to match aspect ratios
            aspectFitter.aspectRatio = NatCam.Preview.width / (float) NatCam.Preview.height;
            // Enable the switch camera button
            switchCamButton.gameObject.SetActive(true);
            // Enable the flash button
            flashButton.gameObject.SetActive(true);
            
            string filePath = Path.Combine(Helper.GetDataPath(), _TEMP_PHOTO_PATH, $"{Random.Range(0, 100)}.png");
            byte[] bytes = photo.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
            
            // Free the photo texture
            Destroy(photo);
            photo = null;
            bytes = null;

            Resources.UnloadUnusedAssets();
        }
        
        private const string _TEMP_PHOTO_PATH = "temp/photos";

        #endregion


        #region --UI Ops--

        public virtual void CapturePhoto()
        {
            // Divert control if we are checking the captured photo
            if (!checkIco.gameObject.activeInHierarchy)
                NatCam.CapturePhoto(OnPhoto);
            // Check captured photo
            else OnView();
        }

        public void SwitchCamera()
        {
            // Switch camera
            if (NatCam.Camera.IsFrontFacing)
                NatCam.StartPreview(DeviceCamera.RearCamera, OnStart);
            else
                NatCam.StartPreview(DeviceCamera.FrontCamera, OnStart);
        }

        public void ToggleFlashMode()
        {
            // Set the active camera's flash mode
            if (NatCam.Camera.IsFlashSupported)
                switch (NatCam.Camera.FlashMode)
                {
                    case FlashMode.Auto:
                        NatCam.Camera.FlashMode = FlashMode.On;
                        break;
                    case FlashMode.On:
                        NatCam.Camera.FlashMode = FlashMode.Off;
                        break;
                    case FlashMode.Off:
                        NatCam.Camera.FlashMode = FlashMode.Auto;
                        break;
                }
            // Set the flash icon
            SetFlashIcon();
        }

        public void FocusCamera(BaseEventData e)
        {
            // Get the touch position in viewport coordinates
            var eventData = e as PointerEventData;
            RectTransform transform = eventData.pointerPress.GetComponent<RectTransform>();
            Vector3 worldPoint;
            if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(transform, eventData.pressPosition,
                eventData.pressEventCamera, out worldPoint))
                return;
            var corners = new Vector3[4];
            transform.GetWorldCorners(corners);
            var point = worldPoint - corners[0];
            var size = new Vector2(corners[3].x, corners[1].y) - (Vector2) corners[0];
            Vector2 relativePoint = new Vector2(point.x / size.x, point.y / size.y);
            // Set the focus point
            NatCam.Camera.FocusPoint = relativePoint;
        }

        #endregion


        #region --Utility--

        private void SetFlashIcon()
        {
            // Set the icon
            bool supported = NatCam.Camera.IsFlashSupported;
            flashIco.color = !supported || NatCam.Camera.FlashMode == FlashMode.Off
                ? (Color) new Color32(120, 120, 120, 255)
                : Color.white;
            // Set the auto text for flash
            flashText.text = supported && NatCam.Camera.FlashMode == FlashMode.Auto ? "A" : "";
        }

        #endregion
    }
}
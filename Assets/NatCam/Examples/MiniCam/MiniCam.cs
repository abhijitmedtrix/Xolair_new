﻿/* 
*   NatCam
*   Copyright (c) 2018 Yusuf Olokoba
*/

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
        }

        private void OnPhoto(Texture2D photo)
        {
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
            
            /*
            // need to test how much memory will we save in that case
            photo.Resize(photo.width / 4, photo.height / 4);
            photo.Apply();
            */
            
            // Free the photo texture
            Texture2D.Destroy(photo);
            photo = null;
        }

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
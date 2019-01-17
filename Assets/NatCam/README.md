# NatCam API
NatCam provides a clean, functional, and amazingly performant API for accessing and controlling device cameras.

Using NatCam is as simple as calling:
```csharp
NatCam.StartPreview(DeviceCamera.RearCamera, OnStart);
```

The preview is started and the preview texture becomes available when the `startCallback` is invoked. This event is usually used to display the preview texture on a surface:
```csharp
void OnStart () {
    // Display the camera preview
    material.mainTexture = NatCam.Preview;
}
```

## Camera Control
NatCam features a full camera control pipeline for utilizing camera functionality such as focusing, zooming, exposure, and so on. To use this functionality, simply access the properties in the `DeviceCamera` class:
```csharp
DeviceCamera.RearCamera.ExposureBias = 1.3;
```

When the preview is running, the camera can be switched by calling `NatCam.StartPreview` with the new camera (no need to stop the preview before switching).
```csharp
// Switch cameras while the preview is playing
NatCam.StartPreview(DeviceCamera.FrontCamera, OnStart);
```

## Capturing Photos
NatCam also allows for high-resolution photo capture from the camera. To do so, simply call the `CapturePhoto` function with an appropriate callback:
```csharp
NatCam.CapturePhoto(OnPhoto);

void OnPhoto (Texture2D photo) {
    // Do stuff with the photo...
    ...
    // Remember to release the texture when you are done with it so as to avoid memory leak
    Texture2D.Destroy(photo); 
}
```

## Accessing the Preview Data
The camera stream data can be accessed using the `CaptureFrame` API. You can use this API to either capture the current preview frame into a `Texture2D`, or to simply copy the data into a `byte[]`.
```csharp
void OnFrame () {
    // Allocate a preview data buffer
    var previewData = new byte[NatCam.Preview.width * NatCam.Preview.height * 4];
    // Copy the preview data into it
    NatCam.CaptureFrame(previewData);
    // Use the preview data
    // ...
}
```

## Using NatCam with OpenCV
NatCam supports OpenCV with the [OpenCVForUnity](https://assetstore.unity.com/packages/tools/integration/opencv-for-unity-21088) package. Check out the [official examples](https://github.com/EnoxSoftware/NatCamWithOpenCVForUnityExample). Using NatCam with OpenCV is pretty easy. On every frame, simply get the preview data from `CaptureFrame` and copy it into an `OpenCVForUnity.Mat`:
```csharp
void OnFrame () {
    // Allocate a preview data buffer
    var previewData = new byte[NatCam.Preview.width * NatCam.Preview.height * 4];
    // Copy the preview data into it
    NatCam.CaptureFrame(previewData);
    // Create a matrix
    var previewMatrix = new Mat(NatCam.Preview.height, NatCam.Preview.width, CvType.CV_8UC4);
    // Copy the preview data into the matrix
    Utils.copyToMat(previewData, previewMatrix);
    // Use the preview matrix
    // ...
}
```

___

With the simplicity of NatCam, you have the power and speed to create interactive, responsive camera apps. Happy coding!

## Requirements
- On iOS, NatCam requires iOS 8 and up.
- On Android, NatCam requires API level 22 and up.

## Tutorials
1. [Starting Off](https://medium.com/@olokobayusuf/natcam-tutorial-series-1-starting-off-dc3990f5dab6)
2. [Controls](https://medium.com/@olokobayusuf/natcam-tutorial-series-2-controls-d2e2d0738223)
3. [Photos](https://medium.com/@olokobayusuf/natcam-tutorial-series-3-photos-e28361b83cf8)
4. [Preview Data](https://medium.com/@olokobayusuf/natcam-tutorial-series-5-preview-data-9ac36eafd1f0)

## Notes
- On Android, Unity automatically requests camera permissions on app start. This cannot be changed without modifying Unity Android natively.
- On iOS, camera permissions are requested the first time the camera is opened.

## Quick Tips
- Please peruse the included scripting reference in the `Docs` folder.
- To discuss or report an issue, visit Unity forums [here](http://forum.unity3d.com/threads/natcam-device-camera-api.374690/).
- Check out more NatCam examples on Github [here](https://github.com/olokobayusuf?tab=repositories).
- Contact me at [olokobayusuf@gmail.com](mailto:olokobayusuf@gmail.com).

Thank you very much!
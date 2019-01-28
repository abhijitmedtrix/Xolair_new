using UnityEngine;
using UnityEngine.UI;

public class PhotosUiItem : MonoBehaviour, System.IDisposable
{
    [SerializeField] protected RawImage _image;
    [SerializeField] protected Toggle _toggle;
    [SerializeField] protected AspectRatioFitter _fitter;

    public bool isSelected => _toggle.isOn;

    public void SetAspect(float aspect)
    {
        _fitter.aspectRatio = aspect;
    }

    public void SetData(Texture2D texture)
    {
        // toggle off by default
        _toggle.SetValue(false);

        if (texture != null)
        {
            Debug.Log($"Texture w: {texture.width}, h: {texture.height}");
            _image.texture = texture;
            _image.enabled = true;
        }
        else
        {
            _image.enabled = false;
        }
    }

    public void Dispose()
    {
        _image.texture = null;
        _toggle.SetValue(false);
    }

    public Texture2D GetTexture()
    {
        if (_image.texture != null)
        {
            return _image.texture as Texture2D;
        }

        return null;
    }
}
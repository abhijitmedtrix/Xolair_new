using System;
using System.IO;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class PhotosScreen : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private GameObject _prevButton, _nextButton;
    [SerializeField] private Text _dateText;
    [SerializeField] private Text _pagesText;

    private Texture2D _currentTexture;
    private int _currentIndex = 0;
    private string[] _texturesPaths;

    public void Show(string[] texturesPaths, DateTime date)
    {
        _currentTexture = new Texture2D(2, 2);
        
        // show previous screen
        ScreenManager.Instance.Set(30);

        _dateText.text = date.ToString("d MMM");

        _texturesPaths = texturesPaths;
        _currentIndex = 0;

        ShowByIndex(_currentIndex);
    }

    public async void Hide()
    {
        if (_currentTexture != null)
        {
            Destroy(_currentTexture);
        }

        _texturesPaths = null;

        // wait for memory cleanup to avoid ui transition lags
        await Resources.UnloadUnusedAssets();

        // show previous screen
        ScreenManager.Instance.Back();
    }

    public void Next()
    {
        if (_currentIndex < _texturesPaths.Length - 1)
        {
            _currentIndex++;
            ShowByIndex(_currentIndex);
        }
    }

    public void Prev()
    {
        if (_currentIndex >= 0)
        {
            _currentIndex--;
            ShowByIndex(_currentIndex);
        }
    }

    private void ShowByIndex(int index)
    {
        _currentTexture.LoadImage(File.ReadAllBytes(_texturesPaths[index]));
        _rawImage.texture = _currentTexture;

        // show/hide buttons
        _nextButton.gameObject.SetActive(index < _texturesPaths.Length - 1);
        _prevButton.gameObject.SetActive(index > 0);

        _pagesText.text = $"{_currentIndex + 1}/{_texturesPaths.Length}";
    }
}
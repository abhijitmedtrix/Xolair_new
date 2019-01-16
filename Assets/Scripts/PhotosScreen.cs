using System;
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

    private int _currentIndex = 0;
    private Texture2D[] _textures;

    public void Show(Texture2D[] textures, DateTime date)
    {
        // show previous screen
        ScreenManager.Instance.Set(30);

        _dateText.text = date.ToString("d MMM");

        _textures = textures;
        _currentIndex = 0;

        ShowByIndex(_currentIndex);
    }

    public async void Hide()
    {
        if (_textures != null)
        {
            for (int i = 0; i < _textures.Length; i++)
            {
                Destroy(_textures[i]);
            }
        }

        _textures = null;

        // wait for memory cleanup to avoid ui transition lags
        await Resources.UnloadUnusedAssets();
        
        // show previous screen
        ScreenManager.Instance.Back();
    }

    public void Next()
    {
        if (_currentIndex < _textures.Length - 1)
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
        _rawImage.texture = _textures[index];

        // show/hide buttons
        _nextButton.gameObject.SetActive(index < _textures.Length - 1);
        _prevButton.gameObject.SetActive(index > 0);

        _pagesText.text = $"{_currentIndex + 1}/{_textures.Length}";
    }
}
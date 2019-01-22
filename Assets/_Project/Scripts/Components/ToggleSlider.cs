using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ToggleSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject[] _activeObjects, _inactiveObjects;
    [SerializeField] private Image[] _imagesToTint;

    private PointerEventData _pointerDownEventData;
    private bool _sliderValueChanged;
    private bool _isAcive => Mathf.Approximately(_slider.value, 1);

    public event Action<bool> OnToggleChange;

    private void Awake()
    {
        if (_slider == null)
        {
            _slider = GetComponent<Slider>();
        }

        _slider.onValueChanged.AddListener(SliderValueChanged);

        UpdateView();
    }

    /// <summary>
    /// Called only by script.
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        _slider.SetValue(active ? 1 : 0);

        UpdateView();

        OnToggleChange?.Invoke(_isAcive);
    }

    /// <summary>
    /// Gray out slider if needed to show that is inactive.
    /// </summary>
    /// <param name="enable"></param>
    public void SetInteractive(bool enable)
    {
        _slider.interactable = enable;

        // tint in/out all objects
        Color col = enable ? Color.white : Color.grey;

        for (int i = 0; i < _imagesToTint.Length; i++)
        {
            _imagesToTint[i].color = col;
        }
    }

    private void SliderValueChanged(float value)
    {
        // Debug.Log("SliderValueChanged: " + value);

        _sliderValueChanged = true;

        UpdateView();

        OnToggleChange?.Invoke(_isAcive);
    }

    private void UpdateView()
    {
        bool isActive = !Mathf.Approximately(_slider.value, 0);

        for (int i = 0; i < _activeObjects.Length; i++)
        {
            _activeObjects[i].SetActive(isActive);
        }

        for (int i = 0; i < _inactiveObjects.Length; i++)
        {
            _inactiveObjects[i].SetActive(!isActive);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_slider.interactable) return;

        _pointerDownEventData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_slider.interactable) return;

        // if that was click
        if (!_sliderValueChanged && (eventData.clickTime - _pointerDownEventData.clickTime) < 0.4f &&
            (_pointerDownEventData.position - eventData.position).magnitude < 10)
        {
            SetActive(!_isAcive);
        }

        _sliderValueChanged = false;
    }
}
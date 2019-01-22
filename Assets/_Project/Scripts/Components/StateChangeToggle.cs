using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This toggle will be used for cases when inactive toggle state must be hidden, instead of overlayed by active sprite.
/// </summary>
[RequireComponent(typeof(Toggle))]
public class StateChangeToggle : MonoBehaviour
{
    [SerializeField] protected GameObject[] _activeImages, _inactiveImages;
    
    public Toggle toggle;
    public Action<StateChangeToggle, bool> OnToggleChange;

    protected virtual void Awake()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }
        
        toggle.onValueChanged.AddListener(OnToggled);
    }

    private void Start()
    {
        UpdateView();
    }

    /// <summary>
    /// Use it to change Toggle state without triggering onValueChanged event.
    /// </summary>
    /// <param name="isOn"></param>
    public void SetValue(bool isOn)
    {
        toggle.SetValue(isOn);
        UpdateView();
    }

    protected void UpdateView()
    {
        for (int i = 0; i < _activeImages.Length; i++)
        {
            _activeImages[i].SetActive(toggle.isOn);
            _inactiveImages[i].SetActive(!toggle.isOn);
        }
    }
    
    protected virtual void OnToggled(bool isOn)
    {
        UpdateView();
        
        OnToggleChange?.Invoke(this, isOn);
    }
}
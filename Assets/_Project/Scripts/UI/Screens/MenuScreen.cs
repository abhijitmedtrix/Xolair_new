using System;
using System.Threading.Tasks;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private Toggle[] _toggles;
    [SerializeField] private float _toggleDelay = 0.5f;
        
    async public void OnCSUSelected(bool isOn)
    {
        if (isOn)
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = false;
            }
            
            await Task.Delay(TimeSpan.FromSeconds(_toggleDelay));

            _toggleGroup.SetAllTogglesOff();
            
            AppManager.Instance.SetMode(AppMode.CSU);
            
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = true;
            }
        }
    }

    async public void OnSAASelected(bool isOn)
    {
        if (isOn)
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = false;
            }
            
            await Task.Delay(TimeSpan.FromSeconds(_toggleDelay));
            
            _toggleGroup.SetAllTogglesOff();

            AppManager.Instance.SetMode(AppMode.SAA);
            
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = true;
            }
        }
    }
}
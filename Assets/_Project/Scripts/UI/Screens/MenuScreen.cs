using System;
using System.Threading.Tasks;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private Toggle[] _toggles;
        
    async public void OnCSUSelected(bool isOn)
    {
        if (isOn)
        {
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = false;
            }
            
            await Task.Delay(TimeSpan.FromSeconds(1));

            ScreenManager.Instance.Set(3);
            _toggleGroup.SetAllTogglesOff();
            
            AppManager.Instance.SetMode(AppManager.Mode.CSU);
            
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
            
            await Task.Delay(TimeSpan.FromSeconds(1));
            
            ScreenManager.Instance.Set(2);
            _toggleGroup.SetAllTogglesOff();

            AppManager.Instance.SetMode(AppManager.Mode.SAA);
            
            for (int i = 0; i < _toggles.Length; i++)
            {
                _toggles[i].interactable = true;
            }
        }
    }
}
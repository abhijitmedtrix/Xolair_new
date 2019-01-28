using UnityEngine;

public class ModeToggle : StateChangeToggle
{
    protected override void Awake()
    {
        base.Awake();
        
        AppManager.OnModeChange += OnModeChange;
    }
    
    private void OnDestroy()
    {
        AppManager.OnModeChange -= OnModeChange;
    }

    private void OnModeChange(AppMode appMode)
    {
        // left position (inactive) is for CSU and right position (active) is for SAA
        SetValue(appMode == AppMode.SAA);
    }
    
    protected override void OnToggled(bool isOn)
    {
        base.OnToggled(isOn);
        
        AppManager.Instance.SetMode(isOn ? AppMode.SAA : AppMode.CSU);
    }
}
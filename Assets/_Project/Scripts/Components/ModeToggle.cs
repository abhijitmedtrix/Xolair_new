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

    private void OnModeChange(AppManager.Mode mode)
    {
        // left position (inactive) is for CSU and right position (active) is for SAA
        SetValue(mode == AppManager.Mode.SAA);
    }
    
    protected override void OnToggled(bool isOn)
    {
        base.OnToggled(isOn);
        
        AppManager.Instance.SetMode(isOn ? AppManager.Mode.SAA : AppManager.Mode.CSU);
    }
}
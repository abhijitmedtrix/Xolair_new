using System;
using UnityEngine;

public class BodyPartToggle : StateChangeToggle
{
    public BodyPart bodyPart;

    public event Action<BodyPart> OnSelected;
    
    protected override void OnToggled(bool isOn)
    {
        base.OnToggled(isOn);

        // Debug.Log("Object been toggled. Is On? "+isOn, gameObject);
        
        if (isOn)
        {
            OnSelected?.Invoke(bodyPart);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class OptionToggle : MonoBehaviour
{
    [SerializeField] private Text _text;

    public Toggle toggle;
    public bool IsOn => toggle.isOn;

    private void Awake()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }
    }

    public void SetAnswer(string str)
    {
        SetValue(false);

        _text.text = str;
    }

    public void SetValue(bool active)
    {
        // set value without triggering toggle event
        toggle.SetValue(active);
    }
}
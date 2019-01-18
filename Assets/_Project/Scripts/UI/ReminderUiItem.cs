using App.Data.Reminders;
using UnityEngine;
using UnityEngine.UI;

public class ReminderUiItem : MonoBehaviour
{
    [SerializeField] protected Text _titleText;
    [SerializeField] protected Text _dateText;

    public ReminderData data;

    public void SetData(ReminderData data)
    {
        this.data = data;
        
    }
}
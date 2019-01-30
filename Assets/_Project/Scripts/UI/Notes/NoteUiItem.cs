using System;
using App.Data;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class NoteUiItem : EnhancedScrollerCellView
{
    public enum State
    {
        Normal,
        Selection
    }

    [SerializeField] private Text _titleText;
    [SerializeField] private Text _dateTitleText;
    [SerializeField] private GameObject _checkbox;
    [SerializeField] private GameObject _checkboxActiveIcon;

    private State _state;
    public NoteData data;
    public bool isSelected => data.isSelected;
    
    [DisplayWithoutEdit] public int index;

    public event Action<NoteUiItem> OnLongClick;
    public event Action<NoteUiItem> OnClick;
    public event Action<NoteUiItem, bool> OnSelect;

    public void SetData(NoteData data)
    {
        Dispose();
        this.data = data;
        this.data.OnDataUpdate += DataUpdate;
        UpdateView();
    }

    private void DataUpdate(NoteData data)
    {
        UpdateView();
    }

    public void Dispose()
    {
        _dateTitleText.gameObject.SetActive(false);
        if (data != null)
        {
            data.OnDataUpdate -= DataUpdate;
            data = null;
        }

        _checkbox.SetActive(false);
        _checkboxActiveIcon.SetActive(false);

        _titleText.text = "";
    }

    protected virtual void UpdateView()
    {
        _titleText.text = data.noteText;

        if (data.hasDateTitle)
        {
            _dateTitleText.gameObject.SetActive(true);
            _dateTitleText.text = data.GetDate().ToString("d MMMM, yyyy");
        }
    }

    public void SetState(State state)
    {
        _state = state;

        if (_state == State.Normal)
        {
            _checkbox.SetActive(false);
            _checkboxActiveIcon.SetActive(false);
        }
        else if (_state == State.Selection)
        {
            _checkbox.SetActive(true);
            _checkboxActiveIcon.SetActive(data.isSelected);
        }
    }

    public void ClickHandler()
    {
        if (_state == State.Normal)
        {
            OnClick?.Invoke(this);
        }
        else if (_state == State.Selection)
        {
            data.isSelected = !data.isSelected;
            _checkboxActiveIcon.SetActive(data.isSelected);
            
            OnSelect?.Invoke(this, data.isSelected);
        }
    }

    public void LongClickHandler()
    {
        if (_state == State.Normal)
        {
            OnLongClick?.Invoke(this);

            // and autoselect
            ClickHandler();
        }
        else if (_state == State.Selection)
        {
        }
    }
}
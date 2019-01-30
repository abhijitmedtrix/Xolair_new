using System;
using System.Collections.Generic;
using App.Data;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class NotesScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    private enum State
    {
        Default,
        Selection,
        Edit
    }

    [SerializeField] private ScreenConfig _screenConfig;
    [SerializeField] private EnhancedScroller _scroller;
    [SerializeField] private EnhancedScrollerCellView _cellViewPrefab;
    [SerializeField] private Text _topBarTitle;
    [SerializeField] private Text _editScrenTopBarTitle;
    [SerializeField] private GameObject _addAndEditScreen;
    [SerializeField] private GameObject _deleteButton, _addButton;
    [SerializeField] private InputField _noteInput;

    private List<NoteData> _data;
    private List<NoteData> _selectedNotesData = new List<NoteData>();
    private NoteData _selectedNoteData;

    private DateTime _targetDate;
    private State _state;

    private void Start()
    {
        _screenConfig.OnShowStarted += ShowStarted;
    }

    private void ShowStarted()
    {
        _state = State.Default;
        _selectedNoteData = null;
    }

    /// <summary>
    /// Default loadout showing all the list of the notes.
    /// </summary>
    public void LoadData()
    {
        _targetDate = DateTime.MinValue;

        NotesManager.OnNotesUpdate += NotesManagerOnNotesUpdate;

        _data = NotesManager.GetNoteData();

        _topBarTitle.text = "Notes";

        SetScroller(_data);

        SetState(State.Default);
    }

    /// <summary>
    /// Loadout showing just particular date notes.
    /// </summary>
    /// <param name="date"></param>
    public void LoadData(DateTime date)
    {
        _targetDate = date;

        NotesManager.OnNotesUpdate += NotesManagerOnNotesUpdate;

        _data = NotesManager.GetNoteData(_targetDate);

        _topBarTitle.text = date.ToString("d MMMM, yyyy");

        SetScroller(_data);

        SetState(State.Default);
    }

    private void NotesManagerOnNotesUpdate()
    {
        if (_targetDate != DateTime.MinValue)
        {
            LoadData(_targetDate);
        }
        else
        {
            LoadData();
        }
    }

    private void SetScroller(List<NoteData> data)
    {
        // set up the scroller delegates
        _scroller.Delegate = this;
        _scroller.scrollerScrolled = ScrollerScrolled;
        _scroller.cellViewInstantiated = CellViewInstantiated;

        // set scroller data
        SetData(data);
    }

    private void SetData(List<NoteData> datas)
    {
        _data = datas;
        Debug.Log("Data count: " + _data.Count);
        _scroller.ReloadData();
    }

    private void SetState(State state)
    {
        _state = state;

        if (_state == State.Default)
        {
            _selectedNotesData.Clear();
            _selectedNoteData = null;
            _addAndEditScreen.SetActive(false);
            _deleteButton.SetActive(false);
            _addButton.SetActive(true);

            SmallList<EnhancedScrollerCellView> list = _scroller.GetActiveCellViews();
            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as NoteUiItem).SetState(NoteUiItem.State.Normal);
            }
        }
        else if (_state == State.Selection)
        {
            _deleteButton.SetActive(true);
            _addButton.SetActive(false);
            SmallList<EnhancedScrollerCellView> list = _scroller.GetActiveCellViews();
            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as NoteUiItem).SetState(NoteUiItem.State.Selection);
            }
        }
        else if (_state == State.Edit)
        {
            if (_selectedNoteData != null)
            {
                _editScrenTopBarTitle.text = _selectedNoteData.GetDate().ToString("d MMMM, yyyy");
                _noteInput.text = _selectedNoteData.noteText;
            }
            else
            {
                _editScrenTopBarTitle.text = _targetDate.ToString("d MMMM, yyyy");
                _noteInput.text = "";
            }

            _addAndEditScreen.SetActive(true);

            // activate input field and show mobile keyboard automatically
            _noteInput.ActivateInputField();
            _noteInput.MoveTextEnd(false);
        }
    }

    #region UI methods

    public void OpenEditNoteScreen()
    {
        SetState(State.Edit);
    }

    public void CloseEditNoteScreen()
    {
        SetState(State.Default);
    }

    public void DeleteSelectedNotes()
    {
        NotesManager.DeleteNotes(_selectedNotesData);
        _selectedNotesData.Clear();
    }

    public void SaveNote()
    {
        // don't allow save empty note
        if (string.IsNullOrEmpty(_noteInput.text))
        {
            return;
        }

        // if we didn't edit existing note
        if (_selectedNoteData == null)
        {
            // add new one
            NoteData data = new NoteData(DateTime.Now);
            data.EditNote(_noteInput.text);

            NotesManager.SaveNote(data);
        }
        else
        {
            // edit existing
            _selectedNoteData.EditNote(_noteInput.text);
        }

        // remove selection
        _selectedNoteData = null;

        CloseEditNoteScreen();
    }

    public void Close()
    {
        NotesManager.OnNotesUpdate -= NotesManagerOnNotesUpdate;
        ScreenManager.Instance.Back();
    }

    #endregion

    #region EnhancedScroller Handlers

    private void CellViewInstantiated(EnhancedScroller enhancedScroller, EnhancedScrollerCellView cellview)
    {
        NoteUiItem item = cellview as NoteUiItem;
        item.OnClick += ItemClickHandler;
        item.OnLongClick += ItemLongClickHandler;
        item.OnSelect += ItemSelectHandler;
    }

    private void ItemClickHandler(NoteUiItem item)
    {
        if (_state == State.Default)
        {
            _selectedNoteData = item.data;
            SetState(State.Edit);
        }
    }

    private void ItemLongClickHandler(NoteUiItem item)
    {
        if (_state == State.Default)
        {
            SetState(State.Selection);
        }
    }

    private void ItemSelectHandler(NoteUiItem item, bool selected)
    {
        if (selected)
        {
            _selectedNotesData.Add(item.data);
        }
        else
        {
            _selectedNotesData.Remove(item.data);

            // if last element been deselected, change state to default
            if (_selectedNotesData.Count == 0)
            {
                SetState(State.Default);
            }
        }
    }

    private void ScrollerScrolled(EnhancedScroller enhancedScroller, Vector2 val, float scrollposition)
    {
    }

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // in this example, we just pass the number of our data elements
        return _data.Count;
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return _data[dataIndex].hasDateTitle ? 200 : 160;
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        NoteUiItem cellView = scroller.GetCellView(_cellViewPrefab) as NoteUiItem;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // update data and view
        cellView.SetData(_data[dataIndex]);

        // set state
        if (_state == State.Default)
        {
            cellView.SetState(NoteUiItem.State.Normal);
        }
        else if (_state == State.Selection)
        {
            cellView.SetState(NoteUiItem.State.Selection);
        }

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}
using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class PhotosScrollController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] protected RectTransform _scrollerRectTransform;

    private float _cellHeigth;
    private List<Texture2D> _data = new List<Texture2D>();

    public PhotosBlockUiItem cellViewPrefab;
    public EnhancedScroller scroller;

    private void Start()
    {
        // Here we modify cell prefab according to device screen parameters. So each new initiated cell will have proper properties.
        
        // update aspect top fit the screen
        float screenAspectRatio = (float) Screen.width / Screen.height;
        cellViewPrefab.SetAspect(screenAspectRatio);
        
        if (Screen.width < 900)
        {
            // leave only 2 items in a block
            cellViewPrefab.SetNumOnItems(2);
        }

        float spacing = _scrollerRectTransform.rect.width * 0.075f;
        
        // update scroller vertical spacing
        scroller.spacing = spacing;
        
        // define proper cell size to fit elements well
        float singleItemWidth = _scrollerRectTransform.rect.width - (spacing * (cellViewPrefab.numOfItems - 1)) / cellViewPrefab.numOfItems;

        // Debug.Log("singleItemWidth: "+singleItemWidth);
        
        _cellHeigth = singleItemWidth / screenAspectRatio;
        // Debug.Log("Cell height: "+_cellHeigth);
    }

    private void ScrollerScrolled(EnhancedScroller enhancedScroller, Vector2 val, float scrollposition)
    {
        // Debug.Log($"_lastMiddleCellIndex: {_lastMiddleCellIndex}, closest cell: {scroller.GetClosestCell()}");
        int focusedCellIndex = scroller.GetClosestCell();

        // check is start and end cell items indexes are in a range of 7 days and value not equals to previously cached value
        // if (_lastMiddleCellIndex != focusedCellIndex && focusedCellIndex - 3 >= 0 && focusedCellIndex + 3 < _data.Count)
        // {
        // _lastMiddleCellIndex = scroller.GetClosestCell();

        // update date range
        // SetDateRange(_lastMiddleCellIndex);
        // }

        // for debug only
        /*
        if (scroller.GetCellViewAtDataIndex(_lastMiddleCellIndex) != null)
        {
            CalendarScrollItemView itemView =
                scroller.GetCellViewAtDataIndex(_lastMiddleCellIndex) as CalendarScrollItemView;
            Debug.Log("Get closest point: " + _lastMiddleCellIndex
                                            + " with data " + itemView.data.ToShortDateString());
        }
        else
        {
            Debug.Log("Get closest point: " + _lastMiddleCellIndex);
        }
        */
    }

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // in this example, we just pass the number of our data elements
        return Mathf.FloorToInt(_data.Count / cellViewPrefab.numOfItems);
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
        // Debug.Log($"Screen width: {Screen.width}, _rectTransform size delta: {_scrollerRectTransform.sizeDelta} and rect size: {_scrollerRectTransform.rect.size}");
        return _cellHeigth;
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
        PhotosBlockUiItem cellView = scroller.GetCellView(cellViewPrefab) as PhotosBlockUiItem;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        Texture2D[] data = new Texture2D[cellViewPrefab.numOfItems];

        // split to blocks
        int startIndex = dataIndex * data.Length;

        for (int i = 0; i < data.Length; i++)
        {
            if (startIndex + i < _data.Count)
            {
                data[i] = _data[startIndex + i];
            }
            else
            {
                data[i] = null;
            }
        }

        cellView.SetData(data);

        // return the cell to the scroller
        return cellView;
    }

    #endregion

    public void SetData(List<Texture2D> datas)
    {
        _data = datas;

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();

        scroller.Delegate = this;
        scroller.scrollerScrolled = ScrollerScrolled;
    }
    
    public void Dispose()
    {
        _data = null;
        
        scroller.Delegate = null;
        scroller.scrollerScrolled = null;
    }

    public Texture2D[] GetSelectedPhotos()
    {
        List<Texture2D> texturesList = new List<Texture2D>();
        SmallList<EnhancedScrollerCellView> activeItems = scroller.GetActiveCellViews();

        PhotosUiItem[] items;
        for (int i = 0; i < activeItems.Count; i++)
        {
            PhotosBlockUiItem blockUiItem = activeItems[i].GetComponent<PhotosBlockUiItem>();
            items = blockUiItem.GetSelectedItems();

            for (int j = 0; j < items.Length; j++)
            {
                texturesList.Add(items[j].GetTexture());
            }
        }

        return texturesList.ToArray();
    }
}
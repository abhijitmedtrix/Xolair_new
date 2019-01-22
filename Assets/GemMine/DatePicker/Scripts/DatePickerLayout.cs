using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DatePickerLayout : MonoBehaviour
{
    public PickerCell.CellClicked cellClickedDelegate;
    
    // hiw many rows and columns has the grid
    public int columns;
    public int rows;
    public int horizSpacing;
    public int vertSpacing;

    // the grid consists of buttons
    public GameObject button;

    // the color scheme
    public Sprite otherEntryImg;
    public Sprite currentEntryImg;
    public Sprite actualEntryImg;

    public Color fontActiveColor;
    public Color fontInactiveColor;
    public Color fontSelectedColor;

    public Calendar calendar;

    // array to store the found buttons
    protected List<PickerCell> cells = new List<PickerCell>();

    public int GetNumberOfCells()
    {
        return columns * rows;
    }

    public void CreateCells(string name)
    {
        RectTransform mainPanelRect = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        // create the elements
        for (int i = 0; i < columns * rows; i++)
        {
            GameObject go = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;
            PickerCell cell = go.GetComponent<PickerCell>();
            cell.cellClickedDelegate = CellClicked;
            cells.Add(cell);
            
            go.transform.SetParent(this.transform);
            go.transform.localScale = Vector3.one;
            go.name = name;
        }

        // set the grid layout
        grid.cellSize = new Vector2(mainPanelRect.rect.width / columns - horizSpacing,
            mainPanelRect.rect.height / rows - vertSpacing);
        grid.spacing = new Vector2(horizSpacing, vertSpacing);
    }

    public virtual void SetupCells(DateTime now)
    {
    }

    public virtual void CellClicked(PickerCell cell)
    {
        
        cellClickedDelegate?.Invoke(cell);
    }

    public virtual string GetInfo(DateTime selected)
    {
        return "";
    }

    public void SetSprites(Sprite current, Sprite actual, Sprite other)
    {
        currentEntryImg = current;
        actualEntryImg = actual;
        otherEntryImg = other;
    }

    public void SetFontColors(Color active, Color deactive, Color selected)
    {
        fontActiveColor = active;
        fontInactiveColor = deactive;
        fontSelectedColor = selected;
    }

    public void SetSpacing(int hSpacing, int vSpacing, Color spacingColor)
    {
        horizSpacing = hSpacing;
        vertSpacing = vSpacing;
        GetComponent<Image>().color = spacingColor;
    }
}
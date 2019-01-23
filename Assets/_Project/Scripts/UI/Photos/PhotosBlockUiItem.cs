using System;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class PhotosBlockUiItem : EnhancedScrollerCellView, IDisposable
{
    private PhotosUiItem[] _items;

    public int numOfItems => _items.Length;
    
    private void Awake()
    {
        _items = GetComponentsInChildren<PhotosUiItem>(true);
    }

    /// <summary>
    /// Called once in a prefab instance after screen width check
    /// </summary>
    /// <param name="num"></param>
    public void SetNumOnItems(int num)
    {
        if (num == 2)
        {
            Destroy(_items[2].gameObject);
            Array.Resize(ref _items, 2);
            // Debug.Log("Array length: "+_items.Length);
        }
    }

    public void SetAspect(float aspect)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetAspect(aspect);
        }
    }

    public void SetData(Texture2D[] textures)
    {
        if (_items.Length != textures.Length)
        {
            throw new Exception("Wrong textures data count provided!");
        }

        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetData(textures[i]);
        }
    }

    public PhotosUiItem[] GetSelectedItems()
    {
        return _items.Where(x => x.isSelected).ToArray();
    }

    public void Dispose()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].Dispose();
        }
    }
}
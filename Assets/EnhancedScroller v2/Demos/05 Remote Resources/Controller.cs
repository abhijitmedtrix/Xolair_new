﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;

namespace EnhancedScrollerDemos.RemoteResourcesDemo
{
    /// <summary>
    /// This demo shows how you can remotely load resources, calling the set data function when
    /// the cell's visibility changes to true. When the cell is hidden, we set the image back to
    /// a default loading sprite.
    /// </summary>
    public class Controller : MonoBehaviour, IEnhancedScrollerDelegate
    {
        /// <summary>
        /// The data for the scroller
        /// </summary>
        private SmallList<Data> _data;

        /// <summary>
        /// The scroller to control
        /// </summary>
        public EnhancedScroller scroller;

        /// <summary>
        /// The prefab of the cell view
        /// </summary>
        public EnhancedScrollerCellView cellViewPrefab;

        /// <summary>
        /// The location of a file that contains a list of image urls in json format.
        /// We could supply an array of image urls here, but this gives an extra example
        /// of how to pull in remote data to load the image locations, then download the
        /// individual images later.
        /// </summary>
        public string imageListURL;

        void Start()
        {
            // set the scroller's cell view visbility changed delegate to a method in this controller
            scroller.cellViewVisibilityChanged = CellViewVisibilityChanged;

            // start a coroutine to download a text file
            // that contains image urls and sizes
            StartCoroutine(LoadImageList());
        }

        /// <summary>
        /// Download a text file with images and sizes in it,
        /// then set up the scroller's data with those images.
        /// You could supply your image urls directly, but this
        /// gives an extra example of pulling in data.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadImageList()
        {
            // download the image list text file
            WWW www = new WWW(imageListURL);
            yield return null;
            while (!www.isDone) { }

            // parse the image list from json to an array of objects
            var imageList = JsonUtility.FromJson<RemoteImageList>(www.text);

            // set up some simple data
            _data = new SmallList<Data>();

            // set up a list of images with their dimensions
            for (var i = 0; i < imageList.images.Length; i++)
            {
                // add the image based on the image list text file
                _data.Add(new Data()
                {
                    imageUrl = imageList.images[i].url,
                    imageDimensions = new Vector2(imageList.images[i].size.x, imageList.images[i].size.y)
                });
            }

            // set the scroller's delegate to this controller
            scroller.Delegate = this;

            // tell the scroller to reload
            scroller.ReloadData();
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
            // return a fixed cell size of 200 pixels
            return (260f);
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
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

            // set the name of the game object to the cell's data index.
            // this is optional, but it helps up debug the objects in 
            // the scene hierarchy.
            cellView.name = "Cell " + dataIndex.ToString();

            // In this example, we do not set the data here since the cell is not visibile yet. Use a coroutine
            // before the cell is visibile will result in errors, so we defer loading until the cell has
            // become visible. We can trap this in the cellViewVisibilityChanged delegate handled below

            // return the cell to the scroller
            return cellView;
        }

        /// <summary>
        /// This handler will be called any time a cell view is shown or hidden
        /// </summary>
        /// <param name="cellView">The cell view that was shown or hidden</param>
        private void CellViewVisibilityChanged(EnhancedScrollerCellView cellView)
        {
            // cast the cell view to our custom view
            CellView view = cellView as CellView;

            // if the cell is active, we set its data, 
            // otherwise we will clear the image back to 
            // its default state

            if (cellView.active)
                view.SetData(_data[cellView.dataIndex]);
            else
                view.ClearImage();
        }

        #endregion
    }
}
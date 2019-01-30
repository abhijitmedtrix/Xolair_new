using System;
using UnityEngine;

namespace App.Data
{
    public class NoteData : BaseTrackerData
    {
        /// <summary>
        /// Used just for NoteUiItem to keep selection state for EnhancedScroller 
        /// </summary>
        public bool isSelected;

        public Action<NoteData> OnDataUpdate;

        public string noteText;

        public bool isEmpty => string.IsNullOrEmpty(noteText);
        public bool hasDateTitle;

        public NoteData(DateTime date) : base(date)
        {
        }

        public NoteData(string json) : base(json)
        {
            noteText = _jsonObject.GetField("note").str;
            // Debug.Log($"Found note text: {noteText} for date {_dateTime.Date}, original json: {json}");
        }

        public NoteData()
        {
        }

        public void EditNote(string note)
        {
            if (noteText != note)
            {
                noteText = note;
                OnDataUpdate?.Invoke(this);
            }
        }

        public override JSONObject FormatToJson()
        {
            // get created json object with data included
            JSONObject jsonObject = base.FormatToJson();
            jsonObject.AddField("note", noteText);
            return jsonObject;
        }
    }
}
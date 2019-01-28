using System;
using System.Collections.Generic;

namespace App.Data
{
    public class NoteData : BaseTrackerData
    {
        public DateTime dateTime;
        public List<string> notes;

        protected NoteData(DateTime date) : base(date)
        {
        }

        public NoteData(string json) : base(json)
        {
        }

        public NoteData()
        {
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using App.Data;
using App.Utils;
using UnityEngine;

public class NotesManager : MonoSingleton<NotesManager>
{
    public static List<NoteData> notes = new List<NoteData>();
    public static Dictionary<DateTime, NoteData> notesDictionary = new Dictionary<DateTime, NoteData>();
    
    private const string NOTES_LOG = "notes.json";
    private static string _localPath;

    private void Awake()
    {
        // parse notes json file
        string _localPath = Path.Combine(Helper.GetDataPath(), NOTES_LOG);
        
        List<JSONObject> jsonList = TrackerManager.GetJsonList(_localPath);

        for (int i = 0; i < jsonList.Count; i++)
        {
            JSONObject jsonObj = jsonList[i];
            NoteData data = new NoteData(jsonObj.ToString());

            // don't manage outdated logs
            if ((DateTime.Today - data.GetDate()).TotalDays > TrackerManager.LOG_LIFE_TIME) continue;

            notes.Add(data);
            notesDictionary.Add(data.GetDate(), data);
        }
    }

    public static void SaveNote(NoteData data)
    {
        // update log file
        JSONObject o = new JSONObject();
        for (int i = 0; i < notes.Count; i++)
        {
            o.Add(notes[i].FormatToJson());
        }
        
        FileInfo fileInfo = new FileInfo(_localPath);
        fileInfo.Directory.Create();
        File.WriteAllText(_localPath, o.Print(true));
    }
    
    // public static DeleteNote
}
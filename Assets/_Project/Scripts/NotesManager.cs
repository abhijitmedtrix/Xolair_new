using System;
using System.Collections.Generic;
using System.IO;
using App.Data;
using App.Utils;
using QuickEngine.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class NotesManager : MonoSingleton<NotesManager>
{
    private static List<NoteData> _notes = new List<NoteData>();

    private static string[] _notesTemplates = new string[]
        {"Was a great today!", "Too dusty today...", "Hey, just a template note here", "That was the best day ever!"};

    private static string _localPath => Path.Combine(Helper.GetDataPath(), NOTES_LOG);

    private const string NOTES_LOG = "notes.json";

    public static event Action OnNotesUpdate;

    private void Awake()
    {
        List<JSONObject> jsonList = TrackerManager.GetJsonList(_localPath);

        for (int i = 0; i < jsonList.Count; i++)
        {
            JSONObject jsonObj = jsonList[i];
            NoteData data = new NoteData(jsonObj.ToString());

            // don't manage outdated logs
            if ((DateTime.Today - data.GetDate()).Days > TrackerManager.LOG_LIFE_TIME) continue;

            data.OnDataUpdate += DataUpdate;
            _notes.Insert(0, data);
        }
    }

    public static List<NoteData> GetNoteData()
    {
        DateTime lastDate = DateTime.MinValue;
                
        for (int i = 0; i < _notes.Count; i++)
        {
            // Debug.Log($"At index: {i} note with a date: {_notes[i].GetDate()} has data title: {!_notes[i].GetDate().IsSameDay(lastDate)} with {lastDate}");
            _notes[i].hasDateTitle = !_notes[i].GetDate().IsSameDay(lastDate);
            lastDate = _notes[i].GetDate();
        }
        return _notes;
    }

    public static List<NoteData> GetNoteData(DateTime date)
    {
        List<NoteData> datas = _notes.FindAll(x => x.GetDate().IsSameDay(date));
        
        for (int i = 0; i < datas.Count; i++)
        {
            datas[i].hasDateTitle = false;
        }

        return datas;
    }

    private static void DataUpdate(NoteData data)
    {
        if (data.isEmpty)
        {
            DeleteNote(data);
        }
        else
        {
            OnNotesUpdate?.Invoke();
        }
    }

    public static void AddNote(NoteData data)
    {
        _notes.Add(data);
        
        // update log file
        JSONObject o = new JSONObject();
        for (int i = 0; i < _notes.Count; i++)
        {
            o.Add(_notes[i].FormatToJson());
        }

        WriteToFile(_localPath, o.Print(true));
        
        OnNotesUpdate?.Invoke();
    }

    public static void DeleteNote(NoteData data)
    {
        data.OnDataUpdate -= DataUpdate;

        _notes.Remove(data);

        OnNotesUpdate?.Invoke();
    }

    public static void DeleteNotes(List<NoteData> datas)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            datas[i].OnDataUpdate -= DataUpdate;
            _notes.Remove(datas[i]);
        }

        OnNotesUpdate?.Invoke();
    }

    private static void WriteToFile(string path, string content)
    {
        FileInfo fileInfo = new FileInfo(path);
        fileInfo.Directory.Create();
        File.WriteAllText(path, content);
    }

    public static void FillTestData(int days)
    {
        // load random textures form resources
        Texture2D[] textures = Resources.LoadAll<Texture2D>("RandomTextures");

        // int days = 1;
        // stay today date empty to use
        DateTime date = DateTime.Now.AddDays(-days).Date;
        Debug.Log("Fixed start date: " + date.ToString("dd/MM/yyyy"));

        JSONObject notesListObject = new JSONObject();

        for (int i = 0; i < days; i++)
        {
            // don't fill each day
            if (Random.Range(0f, 1f) > 0.5f)
            {
                continue;
            }

            int maxNotes = Random.Range(1, 5);
            for (int j = 0; j < maxNotes; j++)
            {
                NoteData noteData = new NoteData(date);
                noteData.noteText = _notesTemplates[Random.Range(0, _notesTemplates.Length)];
                notesListObject.Add(noteData.FormatToJson());
            }

            // add a single day
            date = date.AddDays(1);
        }

        WriteToFile(_localPath, notesListObject.Print(true));
        Debug.LogWarning($"Random notes generated for {days} days");
    }

    public static void DeleteLogs()
    {
        if (File.Exists(_localPath))
        {
            File.Delete(_localPath);
        }
    }
}
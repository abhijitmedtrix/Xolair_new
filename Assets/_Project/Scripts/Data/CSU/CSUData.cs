using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.Utils;
using UnityEngine;

namespace App.Data.CSU
{
    /*
     * JSON structure
     * "answers" : [
     *     {
     *     "head" : [1, 1],
     *     "chest" : [0, 0],
     *     "legs" : [1, 0]
     *     }
     * ]
     *
     * Photos location
     *
     * Date folder/head/1.png
     * Date folder/head/2.png
     * Date folder/chest/2.png
     * Date folder/legs/2.png
     */

    public class CSUData : QuestionBasedTrackerData
    {
        public override List<QuestionData> questionDataList
        {
            get { return questions; }
        }
        
        public enum BodyPart
        {
            Head,
            Chest,
            Legs
        }

        // set default dictionary values
        private Dictionary<BodyPart, Answer[]> _answersDict;

        private BodyPart _activeBodyPart;

        private const string CSU_FOLDER = "CSU";

        public CSUData(string json) : base(json)
        {
            // set default data values
            _answersDict = new Dictionary<BodyPart, Answer[]>
            {
                {BodyPart.Head, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}},
                {BodyPart.Chest, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}},
                {BodyPart.Legs, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}}
            };
            
            // clear incorrectly filled array from base QuestionBaseTrackerData constructor
            _answers.Clear();

            // parse json
            JSONObject answersObject = _jsonObject.GetField("answers");

            var values = Enum.GetValues(typeof(BodyPart));
            
            // fill up dictionary
            foreach (BodyPart key in values)
            {
                string keyString = key.ToString().ToLower();
                if (answersObject.HasField(keyString))
                {
                    Answer[] answers = new Answer[2];
                    JSONObject obj = answersObject.GetField(keyString);
                    for (int i = 0; i < obj.list.Count; i++)
                    {
                        answers[i] = new Answer{option = (int) obj.list[i].GetField("option").n};
                    }

                    _answersDict[key] = answers;
                    
                }
                else
                {
                    
                    Debug.LogWarning($"Key {key} wasn't found in json {json}");
                }
            }

        }

        public CSUData(DateTime date) : base(date)
        {
            // set default data values
            _answersDict = new Dictionary<BodyPart, Answer[]>
            {
                {BodyPart.Head, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}},
                {BodyPart.Chest, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}},
                {BodyPart.Legs, new Answer[] {new Answer {option = 0}, new Answer {option = 0}}}
            };
        }
        public void Set_indx()
        {
            _currentQuestionIndex = 0;
        }
        public override JSONObject FormatToJson()
        {
            JSONObject jsonObject = base.FormatToJson();
            jsonObject.RemoveField("answers");
            
            JSONObject answersObject = new JSONObject();

            foreach (BodyPart key in Enum.GetValues(typeof(BodyPart)))
            {
                Answer[] answers = _answersDict[key];
                JSONObject objArray = new JSONObject();
                for (int i = 0; i < answers.Length; i++)
                {
                    objArray.Add(answers[i].FormatToJson());
                }

                // add body part answers
                answersObject.AddField(key.ToString().ToLower(), objArray);
            }
            jsonObject.AddField("answers", answersObject);
            return jsonObject;
        }

        public override QuestionData SetAnswer(QuestionData question, int option)
        {
            _currentQuestionIndex = questionDataList.IndexOf(question);

            Answer[] answers = _answersDict[_activeBodyPart];
            answers[_currentQuestionIndex].option = option;

            if (_currentQuestionIndex < questionDataList.Count - 1)
            {
                // increase index, there is one more question
                _currentQuestionIndex++;
                return GetQuestion();
            }

            // returning null will allow to understand that there are no more questions
            return null;
        }

        // for CSU tracker always return true because we can apply any values and submit it
        public override bool IsCompleted()
        {
            return true;
        }

        public override int GetScore()
        {
            _totalScore = 0;

            foreach (KeyValuePair<BodyPart, Answer[]> entry in _answersDict)
            {
                Answer[] answers = entry.Value;
                for (int i = 0; i < answers.Length; i++)
                {
                    _totalScore += questionDataList[i].answersOption[answers[i].option].points;
                }
            }

            return _totalScore;
        }

        public override int GetMaxScore()
        {
            int max = 0;
            
            var values = Enum.GetValues(typeof(BodyPart));
            
            for (int i = 0; i < questionDataList.Count; i++)
            {
                max += questionDataList[i].GetMaxScore();
            }

            // there are 3 body parts for CSU data, so multiply this value x3
            max *= values.Length;

            return max;
        }
        
        public void ChangeBodyPart(int index)
        {
            ChangeBodyPart((BodyPart) index);
        }

        public void ChangeBodyPart(BodyPart bodyPart)
        {
            _activeBodyPart = bodyPart;
        }

        public void SavePhotos(Texture2D[] textures)
        {
            string folderPath = Path.Combine(Helper.GetDataPath(), TrackerManager.LOGS_FOLDER, CSU_FOLDER, this.GetDate().ToString("dd-MM-yyyy"),
                _activeBodyPart.ToString().ToLower());

            for (int i = 0; i < textures.Length; i++)
            {
                string filePath = Path.Combine(folderPath, i + ".png");

                FileInfo file = new FileInfo(Path.Combine(folderPath, i + ".png"));
                file.Directory.Create();
                File.WriteAllBytes(file.FullName, textures[i].EncodeToPNG());
            }
        }

        public Texture2D[] GetPhotos(BodyPart bodyPart)
        {
            string folderPath = Path.Combine(Helper.GetDataPath(), CSU_FOLDER, this.GetDate().ToString("dd-MM-yyyy"),
                bodyPart.ToString().ToLower());

            DirectoryInfo directory = new DirectoryInfo(folderPath);
            if (!directory.Exists)
            {
                Debug.LogWarning("No such directory found: " + directory.FullName);
                return null;
            }

            FileInfo[] files = directory.GetFiles().Where(x => x.FullName.EndsWith(".png")).ToArray();
            if (files.Length == 0)
            {
                Debug.LogWarning("No files with <.png> extension found in directory: " + directory.FullName);
                return null;
            }

            // load all existing textures
            Texture2D[] textures = new Texture2D[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                textures[i] = new Texture2D(2, 2);
                textures[i].LoadImage(File.ReadAllBytes(files[i].FullName));
            }

            return textures;
        }
        
        public int GetPhotosCount()
        {
            string folderPath = Path.Combine(Helper.GetDataPath(), TrackerManager.LOGS_FOLDER, CSU_FOLDER, this.GetDate().ToString("dd-MM-yyyy"));

            DirectoryInfo directory = new DirectoryInfo(folderPath);
            if (!directory.Exists)
            {
                Debug.LogWarning("No such directory found: " + directory.FullName);
                return 0;
            }

            return Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories).Length;
        }
        
        public Texture2D[] GetAllPhotos()
        {
            string folderPath = Path.Combine(Helper.GetDataPath(), TrackerManager.LOGS_FOLDER, CSU_FOLDER, this.GetDate().ToString("dd-MM-yyyy"));

            DirectoryInfo directory = new DirectoryInfo(folderPath);
            if (!directory.Exists)
            {
                Debug.LogWarning("No such directory found: " + directory.FullName);
                return null;
            }
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            // load all existing textures
            Texture2D[] textures = new Texture2D[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                textures[i] = new Texture2D(2, 2);
                textures[i].LoadImage(File.ReadAllBytes(files[i]));
            }

            return textures;
        }

        /// <summary>
        /// Only CSU tracker got local content to remove. Must be triggered from LogManager for logs with life time > max limit (LogManager.LOG_LIFE_TIME)
        /// </summary>
        public void DeletePhotos()
        {
            string folderPath = Path.Combine(Helper.GetDataPath(), CSU_FOLDER, this.GetDate().ToString("dd/MM/yyyy"));
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath);
            }
        }

        public static List<QuestionData> questions = new List<QuestionData>
        {
            new QuestionData()
            {
                question = "Select number of itches",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "None"},
                    new AnswerOption {option = 1, points = 1, description = "Mild"},
                    new AnswerOption {option = 2, points = 2, description = "Moderate"},
                    new AnswerOption {option = 3, points = 3, description = "Severe"}
                }
            },
            new QuestionData()
            {
                question = "Select number of hives",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "None"},
                    new AnswerOption {option = 1, points = 1, description = "1-4"},
                    new AnswerOption {option = 2, points = 2, description = "5-10"},
                    new AnswerOption {option = 3, points = 3, description = ">10"}
                }
            }
        };
    }
}
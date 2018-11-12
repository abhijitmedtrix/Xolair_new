using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Data
{
    public abstract class QuestionBasedTrackerData : BaseTrackerData
    {
        protected List<Answer> _answers = new List<Answer>();
        protected int _currentQuestionIndex = 0;

        public class Answer
        {
            public int option;

            public Answer()
            {
                option = -1;
            }

            public Answer(string json)
            {
                // parse json
                JSONObject obj = new JSONObject(json);
                if (obj.HasField("option"))
                {
                    option = (int) obj.GetField("option").n;
                }
            }

            public JSONObject FormatToJson()
            {
                JSONObject jsonObj = new JSONObject();
                jsonObj.AddField("option", option);
                return jsonObj;
            }
        }

        protected QuestionBasedTrackerData(DateTime date) : base(date)
        {
        }

        public QuestionBasedTrackerData(string json) : base(json)
        {
            List<JSONObject> list = _jsonObject.GetField("answers").list;

            _answers.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                _answers.Add(new Answer(list[i].ToString()));
            }
        }

        public QuestionBasedTrackerData()
        {
        }

        public virtual QuestionData SetAnswer(QuestionData question, int option)
        {
            // get question index
            _currentQuestionIndex = questionDataList.IndexOf(question);

            // create or update existing answer
            
            // update
            if (_currentQuestionIndex < _answers.Count)
            {
                _answers[_currentQuestionIndex].option = option;
            }
            // create new
            else
            {
                _answers.Add(new Answer
                {
                    option = option
                });
            }

            // if there are some more questions after that
            if (_currentQuestionIndex + 1 < questionDataList.Count)
            {
                // define next question index
                int nextQuestionIndex = _currentQuestionIndex + 1;
                
                // if user selected a 1st answer which allow to skip the next question
                if (option == 0 && questionDataList[nextQuestionIndex].canBeSkipped)
                {
                    // create or update next dummy answer with default params
                    if (nextQuestionIndex < _answers.Count)
                    {
                        _answers[nextQuestionIndex].option = option;
                    }
                    // create new default with option set to <-1>
                    else
                    {
                        _answers.Add(new Answer());
                    }
                    
                    Debug.Log("Skipping question with index: "+nextQuestionIndex);
                    
                    // if there are still some questions after skipped 
                    if (nextQuestionIndex + 1 < questionDataList.Count)
                    {
                        // Debug.Log("Current question index: " + _currentQuestionIndex);
    
                        // jump over skipped question if some more questions exists
                        _currentQuestionIndex = nextQuestionIndex + 1;
                    }
                    // otherwise user completed all questions
                    else
                    {
                        // inform listeners about completion
                        OnComplete?.Invoke(GetScore());

                        // returning null will allow to understand that there are no more questions
                        return null;
                    }
                }
                else
                {
                    _currentQuestionIndex++;
                }
                
                // Debug.Log("Current question index: " + _currentQuestionIndex);

                // return next question data
                return GetQuestion();
            }
            else
            {
                // inform listeners about completion
                OnComplete?.Invoke(GetScore());

                // returning null will allow to understand that there are no more questions
                return null;
            }
        }

        public override JSONObject FormatToJson()
        {
            // get created json object with data included
            JSONObject jsonObject = base.FormatToJson();

            JSONObject answersObject = new JSONObject();
            for (int i = 0; i < _answers.Count; i++)
            {
                // just add entry to the list
                answersObject.Add(_answers[i].FormatToJson());
            }

            jsonObject.AddField("answers", answersObject);
            return jsonObject;
        }

        public QuestionData GetQuestion()
        {
            // Debug.Log($"In data {this.GetType().Name} first question is {questionDataList[0].question}");
            return questionDataList[_currentQuestionIndex];
        }

        public virtual bool IsCompleted()
        {
            return _answers.Count == questionDataList.Count;
        }

        /// <summary>
        /// Don't store points in a log file because if something changes (especially on testing) we will need to modify all logs entries.
        /// </summary>
        /// <returns></returns>
        public virtual int GetScore()
        {
            _totalScore = 0;
            // Debug.Log($"question data count: {questionDataList.Count} and answers count: {_answers.Count}");
            for (int i = 0; i < _answers.Count; i++)
            {
                // Debug.Log($"answer option at index <{i}> is {_answers[i].option}");
                
                // in some cases for "empty" answer we just initialise default Result data with default value -1
                if (_answers[i].option > -1)
                {
                    // Debug.Log($"Index: {i}, question answer options count: {questionDataList[i].answersOption.Length}, and selected answer option: {_answers[i].option}");
                    _totalScore += questionDataList[i].answersOption[_answers[i].option].points;
                }
            }

            return _totalScore;
        }

        public virtual int GetMaxScore()
        {
            int max = 0;
            for (int i = 0; i < questionDataList.Count; i++)
            {
                max += questionDataList[i].GetMaxScore();
            }

            return max;
        }

        public abstract List<QuestionData> questionDataList { get; }

        public class QuestionData
        {
            public string question;

            public AnswerOption[] answersOption;

            // this option is actual only for SymptomData, in other cases keep it always <false>
            public bool canBeSkipped;

            public int GetMaxScore()
            {
                return answersOption[answersOption.Length - 1].points;
            }
        }

        public struct AnswerOption
        {
            public int option;
            public int points;
            public string description;
        }
    }
}
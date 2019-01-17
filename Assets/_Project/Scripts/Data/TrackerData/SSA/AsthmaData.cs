using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Data.SSA
{
    public class AsthmaData : QuestionBasedTrackerData
    {
        /// <summary>
        /// Use to parse single day log entry.
        /// </summary>
        /// <param name="json"></param>
        public AsthmaData(string json) : base(json)
        {
        }
        
        public AsthmaData(DateTime date) : base (date)
        {
        }

        public override List<QuestionData> questionDataList
        {
            get { return questions; }
        }
        public void set_indx()
        {
            _currentQuestionIndex = 0;
        }
        public static List<QuestionData> questions = new List<QuestionData>
        {
            new QuestionData()
            {
                question =
                    "On average, during the past week, how often were you woken by your asthma during the night?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "Never"},
                    new AnswerOption {option = 1, points = 1, description = "Hardly ever"},
                    new AnswerOption {option = 2, points = 2, description = "A few times"},
                    new AnswerOption {option = 3, points = 3, description = "Several times"},
                    new AnswerOption {option = 4, points = 4, description = "Many times"},
                    new AnswerOption {option = 5, points = 5, description = "A great many times"},
                    new AnswerOption {option = 6, points = 6, description = "Unable to sleep because of asthma"}
                }
            },
            new QuestionData()
            {
                question =
                    "On average, during the past week, how bad were your asthma symptoms when you woke up in the morning?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "No symptoms"},
                    new AnswerOption {option = 1, points = 1, description = "Very mild symptoms"},
                    new AnswerOption {option = 2, points = 2, description = "Mild symptoms"},
                    new AnswerOption {option = 3, points = 3, description = "Moderate symptoms"},
                    new AnswerOption {option = 4, points = 4, description = "Quite severe symptoms"},
                    new AnswerOption {option = 5, points = 5, description = "Severe symptoms"},
                    new AnswerOption {option = 6, points = 6, description = "Very severe symptoms"}
                }
            },
            new QuestionData()
            {
                question =
                    "In general, during the past week, how limited were you in your activities because of your asthma?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "Not limited at all"},
                    new AnswerOption {option = 1, points = 1, description = "Very slightly limited"},
                    new AnswerOption {option = 2, points = 2, description = "Slightly limited"},
                    new AnswerOption {option = 3, points = 3, description = "Moderately limited"},
                    new AnswerOption {option = 4, points = 4, description = "Very limited"},
                    new AnswerOption {option = 5, points = 5, description = "Extremely limited"},
                    new AnswerOption {option = 6, points = 6, description = "Totally limited"}
                }
            },
            new QuestionData()
            {
                question =
                    "In general, during the past week, how much shortness of breath did you experience because of your asthma?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "None"},
                    new AnswerOption {option = 1, points = 1, description = "A very little"},
                    new AnswerOption {option = 2, points = 2, description = "A little"},
                    new AnswerOption {option = 3, points = 3, description = "A moderate amount"},
                    new AnswerOption {option = 4, points = 4, description = "Quite a lot"},
                    new AnswerOption {option = 5, points = 5, description = "A great deal"},
                    new AnswerOption {option = 6, points = 6, description = "A very great deal"}
                }
            },
            new QuestionData()
            {
                question =
                    "In general, during the past week, how much of the time did you wheeze?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "Not at all"},
                    new AnswerOption {option = 1, points = 1, description = "Hardly any of the time"},
                    new AnswerOption {option = 2, points = 2, description = "A little of the time"},
                    new AnswerOption {option = 3, points = 3, description = "A moderate amount of the time"},
                    new AnswerOption {option = 4, points = 4, description = "A lot of the time"},
                    new AnswerOption {option = 5, points = 5, description = "Most of the time"},
                    new AnswerOption {option = 6, points = 6, description = "All the time"}
                }
            },
            new QuestionData()
            {
                question =
                    "On average, during the past week, how many puffs of short-acting bronchodilator (e.g., Ventolin) have you used each day?",
                answersOption = new AnswerOption[]
                {
                    new AnswerOption {option = 0, points = 0, description = "None"},
                    new AnswerOption {option = 1, points = 1, description = "1–2 puffs most days"},
                    new AnswerOption {option = 2, points = 2, description = "3–4 puffs most days"},
                    new AnswerOption {option = 3, points = 3, description = "5–8 puffs most days"},
                    new AnswerOption {option = 4, points = 4, description = "9–12 puffs most days"},
                    new AnswerOption {option = 5, points = 5, description = "13–16 puffs most days"},
                    new AnswerOption {option = 6, points = 6, description = "More than 16 puffs most days"}
                }
            }
        };
    }
}
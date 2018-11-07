using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Data.CSU
{
    public class UASData : QuestionBasedTrackerData
    {
        /// <summary>
        /// Use to parse single day log entry.
        /// </summary>
        /// <param name="json"></param>
        public UASData(string json) : base(json)
        {
        }
        
        public UASData(DateTime date) : base (date)
        {
        }

        public override List<QuestionData> questionDataList
        {
            get { return questions; }
        }
        public UASData()
        {

        }
        public static List<QuestionData> questions = new List<QuestionData>()
        {
            new QuestionData
            {
                question = "How many wheals did you have in the last 24 hours?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 0, points = 0, description = "None"},
                        new AnswerOption {option = 1, points = 1, description = "1-6"},
                        new AnswerOption {option = 2, points = 2, description = "7-12"},
                        new AnswerOption {option = 3, points = 3, description = ">12"}
                    }
            },
            new QuestionData
            {
                question = "What was the intensity of itching you experienced over the last 24?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 1, points = 1, description = "None"},
                        new AnswerOption {option = 2, points = 2, description = "Mildly"},
                        new AnswerOption {option = 3, points = 3, description = "Moderately"},
                        new AnswerOption {option = 4, points = 4, description = "Severely"}
                    }
            }
        };
    }
}
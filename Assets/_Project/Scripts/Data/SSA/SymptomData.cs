using System;
using System.Collections.Generic;

namespace App.Data.SSA
{
    public class SymptomData : QuestionBasedTrackerData
    {
        /// <summary>
        /// Use to parse single day log entry.
        /// </summary>
        /// <param name="json"></param>
        public SymptomData(string json) : base(json)
        {
        }
        
        public SymptomData(DateTime date) : base (date)
        {
        }
        public SymptomData()
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
            // =================
            // COUGH
            // =================
            new QuestionData
            {
                question = "How many days were you bothered by coughing during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 0, points = 0, description = "Not at all"},
                        new AnswerOption {option = 1, points = 1, description = "1-3 days"},
                        new AnswerOption {option = 2, points = 2, description = "4-7 days"},
                        new AnswerOption {option = 3, points = 3, description = "8-14 days"}
                    }
            },
            new QuestionData
            {
                question = "On average, how severe was your coughing during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 1, points = 1, description = "Mild"},
                        new AnswerOption {option = 2, points = 2, description = "Moderate"},
                        new AnswerOption {option = 3, points = 3, description = "Severe"}
                    },
                canBeSkipped = true
            },

            // =================
            // WHEEZING
            // =================
            new QuestionData
            {
                question = "How many days were you bothered by wheezing during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 0, points = 0, description = "Not at all"},
                        new AnswerOption {option = 1, points = 1, description = "1-3 days"},
                        new AnswerOption {option = 2, points = 2, description = "4-7 days"},
                        new AnswerOption {option = 3, points = 3, description = "8-14 days"}
                    }
            },
            new QuestionData
            {
                question = "On average, how severe was your wheezing during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 1, points = 1, description = "Mild"},
                        new AnswerOption {option = 2, points = 2, description = "Moderate"},
                        new AnswerOption {option = 3, points = 3, description = "Severe"}
                    },
                canBeSkipped = true
            },

            // =================
            // SHORTNESS OF BREATH
            // =================
            new QuestionData
            {
                question = "How many days were you bothered by shortness of breath during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 0, points = 0, description = "Not at all"},
                        new AnswerOption {option = 1, points = 1, description = "1-3 days"},
                        new AnswerOption {option = 2, points = 2, description = "4-7 days"},
                        new AnswerOption {option = 3, points = 3, description = "8-14 days"}
                    }
            },
            new QuestionData
            {
                question = "On average, how severe was your shortness of breath during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 1, points = 1, description = "Mild"},
                        new AnswerOption {option = 2, points = 2, description = "Moderate"},
                        new AnswerOption {option = 3, points = 3, description = "Severe"}
                    },
                canBeSkipped = true
            },

            // =================
            // EPISODES OF AWAKENING AT NIGHT
            // =================
            new QuestionData
            {
                question = "How many days were you awakened at night during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 0, points = 0, description = "Not at all"},
                        new AnswerOption {option = 1, points = 1, description = "1-3 days"},
                        new AnswerOption {option = 2, points = 2, description = "4-7 days"},
                        new AnswerOption {option = 3, points = 3, description = "8-14 days"}
                    }
            },
            new QuestionData
            {
                question = "On average, how severe was your coughing during the past 2 weeks?",
                answersOption =
                    new AnswerOption[]
                    {
                        new AnswerOption {option = 1, points = 1, description = "Mild"},
                        new AnswerOption {option = 2, points = 2, description = "Moderate"},
                        new AnswerOption {option = 3, points = 3, description = "Severe"}
                    },
                canBeSkipped = true
            }
        };
    }
}
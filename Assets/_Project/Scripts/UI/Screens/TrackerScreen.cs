using System;
using System.Collections.Generic;
using App.Data;
using UnityEngine;
using UnityEngine.UI;

public class TrackerScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _scorePanel;
    [SerializeField] protected Text _pointsText;
    [SerializeField] protected Text _questionText;
    [SerializeField] protected GameObject _submitButton;
    [SerializeField] protected TrackerProgressController _progressController;
    
    [Header("Answers references")]
    [SerializeField] protected Transform _answersContainer;
    [SerializeField] protected ToggleGroup _toggleGroup;
    [SerializeField] protected OptionToggle _answerOptionPrefab;

    protected QuestionBasedTrackerData _trackerData;
    protected List<OptionToggle> _optionsTogglesList = new List<OptionToggle>();

    protected void Start()
    {
        _progressController.OnBackClicked += SetPreviousQuestion;
    }

    public virtual void StartTracker()
    {
        // hide score panel
        _scorePanel.SetActive(false);
        
        // reset question index and show 1st question
        _trackerData.ResetQuestionIndex();
        InitiateQuestion(_trackerData.GetQuestion());
    }

    public void NextQuestion(int answerIndex)
    {
        Debug.LogWarning("Next question. Answer index: " + answerIndex);
        var question = _trackerData.GetQuestion();

        try
        {
            QuestionBasedTrackerData.QuestionData nextQuestion = _trackerData.SetAnswer(question, answerIndex);
            InitiateQuestion(nextQuestion);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(
                "Exception caught trying to get new question. Seems there is no more questions for this data. Ex: " +
                ex.Message);

            CompleteTracker();
        }
    }

    protected void InitiateQuestion(QuestionBasedTrackerData.QuestionData question)
    {
        _questionText.text = question.question;

        // update progress UI
        _progressController.UpdateProgress(_trackerData.GetCurrentQuestionIndex(), _trackerData.questionDataList.Count);

        // disable all existing toggle options
        for (int i = 0; i < _optionsTogglesList.Count; i++)
        {
            _optionsTogglesList[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < question.answersOption.Length; i++)
        {
            OptionToggle optionToggle;

            if (_optionsTogglesList.Count < question.answersOption.Length)
            {
                optionToggle = ObjectPool
                    .Instantiate(_answerOptionPrefab.gameObject, Vector3.zero, Quaternion.identity,
                        _answersContainer).GetComponent<OptionToggle>();

                optionToggle.toggle.group = _toggleGroup;
                _optionsTogglesList.Add(optionToggle);
            }
            // if there are too many answers toggles already
            else
            {
                optionToggle = _optionsTogglesList[i];
            }

            // enable each used toggle
            optionToggle.gameObject.SetActive(true);

            // hide submit button
            // _submitButton.SetActive(false);

            // update toggle
            _toggleGroup.SetAllTogglesOff();

            optionToggle.SetAnswer(question.answersOption[i].description);
        }
    }

    protected virtual void CompleteTracker()
    {
        TrackerManager.UpdateEntry(DateTime.Today, _trackerData);

        Debug.Log("Score: " + _trackerData.GetScore());

        _scorePanel.SetActive(true);
        _pointsText.text = _trackerData.GetScore().ToString();
    }

    public void RedoTest()
    {
        // hide score panel
        _scorePanel.SetActive(false);

        // start test again
        StartTracker();
    }

    /// <summary>
    /// Submit answer by clicking the button.
    /// </summary>
    public virtual void SubmitAnswer()
    {
        for (int i = 0; i < _optionsTogglesList.Count; i++)
        {
            if (_optionsTogglesList[i].IsOn)
            {
                NextQuestion(i);
                return;
            }
        }
    }

    public void SetPreviousQuestion()
    {
        int currentQuestionIndex = _trackerData.GetCurrentQuestionIndex();

        // get previous question data
        QuestionBasedTrackerData.QuestionData prevQuestion =
            _trackerData.questionDataList[currentQuestionIndex - 1];

        Debug.Log($"currentQuestionIndex: {currentQuestionIndex}, was previous question skipped? : " +
                  _trackerData.WasQuestionSkipped(prevQuestion));

        int qIndex;

        // if that was a skip able question, check, was it skipped or not
        if (_trackerData.WasQuestionSkipped(prevQuestion))
        {
            // jump over 1 question back
            qIndex = currentQuestionIndex - 2;
        }
        else
        {
            qIndex = currentQuestionIndex - 1;
        }

        // update questions & answers view
        InitiateQuestion(_trackerData.GetQuestion(qIndex));

        // set the previously selected option
        _optionsTogglesList[_trackerData.GetAnswerOption(qIndex)].SetValue(true);

        // _submitButton.SetActive(true);
    }
}
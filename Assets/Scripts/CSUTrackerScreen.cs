using UnityEngine.UI;
using UnityEngine;
using App.Data.CSU;
using System;
using System.Collections.Generic;
using System.Linq;
using App.Data;
using DoozyUI;
using MaterialUI;
using UnityEngine.Events;

public class CSUTrackerScreen : MonoBehaviour
{
    private enum Step
    {
        BodyPartSelection,
        Questions,
        Photo,
        PhotoManage
    }

    [SerializeField] private Slider _slider;
    [SerializeField] private Text _questionText;
    [SerializeField] private BodyPartsController _bodyPartsController;
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private GameObject _bodyPartMenu;
    [SerializeField] private GameObject _step1Content;
    [SerializeField] private GameObject _step2Content;
    [SerializeField] private GameObject _step3Content;
    [SerializeField] private GameObject _step4Content;
    [SerializeField] private PhotosScrollController _photosScrollController;

    [SerializeField] private Step _currentStep;
    private BodyPartToggle[] _bodyPartToggles;
    private BodyPart _selectedBodyPart;
    private int _option;
    private Text[] _sliderOptionsTexts;
    private CSUData _csuData;

    private void Start()
    {
        _bodyPartToggles = _bodyPartMenu.GetComponentsInChildren<BodyPartToggle>(true);

        for (int i = 0; i < _bodyPartToggles.Length; i++)
        {
            _bodyPartToggles[i].OnSelected += OnBodyPartSelected;
        }

        _sliderOptionsTexts = _slider.GetComponentsInChildren<Text>(true);
        _slider.onValueChanged.AddListener(OnSliderValueChange);
    }

    public void StartTracker()
    {
        // reset reference in case if test starts again
        if (_csuData != null)
        {
            _csuData = null;
        }

        _currentStep = Step.BodyPartSelection;

        // show needed content
        ShowContent();

        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _csuData = new CSUData(DateTime.Today);
        
        _csuData.ResetQuestionIndex();

        // enable screen
        ScreenManager.Instance.Set(23);

        // reset other components
        _bodyPartsController.ResetView();
        _toggleGroup.SetAllTogglesOff();
    }

    public void CompleteTracker()
    {
        // save progress
        CSUData originalData =
            TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.CSU) as CSUData;
        originalData.ChangeBodyPart(_selectedBodyPart);
        
        originalData.SetAnswers(_csuData.GetAnswers());

        // save all photos
        Texture2D[] textures = _photosScrollController.GetSelectedPhotos();
        if (textures != null && textures.Length > 0)
        {
            originalData.SavePhotos(_photosScrollController.GetSelectedPhotos());
        }

        TrackerManager.UpdateEntry(DateTime.Today, originalData);

        _csuData = null;

        // open CSU tracker menu dialog asking for another body part data fillup
        ScreenManager.Instance.Set(24);
        
        _photosScrollController.Dispose();
    }

    public void NextStep()
    {
        switch (_currentStep)
        {
            case Step.BodyPartSelection:
                // make sure body part toggle been selected
                if (!_toggleGroup.AnyTogglesOn())
                {
                    Debug.LogWarning("Body part must be selected");
                    return;
                }
                
                _currentStep = Step.Questions;

                _bodyPartsController.SetBodyPart(_selectedBodyPart);

                // load 1st question
                InitiateQuestion(_csuData.GetQuestion());
                break;

            case Step.Questions:
                // if that wasn't a last question
                if (_csuData.GetCurrentQuestionIndex() < _csuData.questionDataList.Count - 1)
                {
                    // show next
                    NextQuestion();
                }
                else
                {
                    _currentStep = Step.Photo;
                }

                break;

            case Step.Photo:
                _currentStep = Step.PhotoManage;
                break;

            case Step.PhotoManage:
                CompleteTracker();
                break;
        }

        ShowContent();
    }

    private void NextQuestion()
    {
        var question = _csuData.GetQuestion();
        _csuData.SetAnswer(question, _option);

        try
        {
            QuestionBasedTrackerData.QuestionData nextQuestion = _csuData.SetAnswer(question, _option);
            InitiateQuestion(nextQuestion);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(
                "Exception caught trying to get new question. Seems there is no more questions for this data. Ex: " +
                ex.Message);
        }
    }

    private void InitiateQuestion(QuestionBasedTrackerData.QuestionData question)
    {
        // question index with 0 is about hives
        if (_csuData.GetCurrentQuestionIndex() == 0)
        {
            _bodyPartsController.SetHives();
        }
        // and itches for index 1
        else if (_csuData.GetCurrentQuestionIndex() == 1)
        {
            _bodyPartsController.SetItches();
        }

        _questionText.text = question.question;

        // set slider to initial position
        _slider.SetValue(0);

        // update slider options text
        for (int i = 0; i < _sliderOptionsTexts.Length; i++)
        {
            _sliderOptionsTexts[i].text = question.answersOption[i].description;
        }
    }

    private void OnBodyPartSelected(BodyPart bodyPart)
    {
        // if it's not a bodypart selection step, notify user that progress can be lost
        if (_currentStep != Step.BodyPartSelection)
        {
            // BUG - there is some kind of Unity bug which trigger Toggle onValueChange event execution while ToggleGroup allowToggleOff property turned off
            // skip if user clicked on a toggle which been already active
            if (_selectedBodyPart == bodyPart) return;

            // set toggle back to last selection
            _toggleGroup.SetAllTogglesOff();
            _bodyPartToggles.Single(x => x.bodyPart == _selectedBodyPart).SetValue(true);

            UIManager.NotificationManager.ShowNotification(
                "TwoOptionsTitleUINotification",
                -1,
                false,
                "Are you sure?",
                "Current progress will be lost if you change a body part",
                null,
                new string[] {"No", "Yes"},
                new string[] {"Stay", "Change"},
                new UnityAction[]
                {
                    null,
                    StartTracker
                }
                );
        }
        else
        {
            _selectedBodyPart = bodyPart;
            _csuData.ChangeBodyPart(_selectedBodyPart);
        }
    }

    private void ShowContent()
    {
        HideAllContent();

        if (_currentStep == Step.BodyPartSelection)
        {
            _step1Content.SetActive(true);
            _bodyPartMenu.SetActive(true);
        }
        else if (_currentStep == Step.Questions)
        {
            _step2Content.SetActive(true);
            _bodyPartMenu.SetActive(true);
        }
        else if (_currentStep == Step.Photo)
        {
            _step3Content.SetActive(true);
            _bodyPartMenu.SetActive(false);
        }
        else if (_currentStep == Step.PhotoManage)
        {
            _step4Content.SetActive(true);
            _bodyPartMenu.SetActive(false);
        }
    }

    private void HideAllContent()
    {
        _step1Content.SetActive(false);
        _step2Content.SetActive(false);
        _step3Content.SetActive(false);
        _step4Content.SetActive(false);
    }

    public void OnSliderValueChange(float value)
    {
        _option = Mathf.FloorToInt(value);

        _bodyPartsController.UpdateView(_option);
    }

    /// <summary>
    /// Method triggered by clicking "Take photo" in a step 3
    /// </summary>
    public void StartCamera()
    {
        CameraManager.OnCameraComplete += CameraManagerOnOnCameraComplete;
    }

    private void CameraManagerOnOnCameraComplete(List<Texture2D> photos)
    {
        CameraManager.OnCameraComplete -= CameraManagerOnOnCameraComplete;

        if (photos == null || photos.Count == 0)
        {
            CompleteTracker();
        }
        else
        {
            Debug.Log("Photos count: " + photos.Count);

            // enable screen
            ScreenManager.Instance.Set(23);

            // load photos to the scroller
            _photosScrollController.SetData(photos);

            // proceed to next step
            NextStep();
        }
    }
}
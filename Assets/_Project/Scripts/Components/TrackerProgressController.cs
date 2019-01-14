using System;
using UnityEngine;
using UnityEngine.UI;

public class TrackerProgressController : MonoBehaviour
{
    [SerializeField] private Text _progressText;
    [SerializeField] private Button _backButton;

    public event Action OnBackClicked;
    
    public void UpdateProgress(int currentQuestion, int totalQuestions)
    {
        _progressText.text = string.Format("{0} of {1}", currentQuestion + 1, totalQuestions);
        _backButton.gameObject.SetActive(currentQuestion != 0);
    }

    public void Back()
    {
        OnBackClicked?.Invoke();
    }
}
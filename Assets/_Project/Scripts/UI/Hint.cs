using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] protected Text _text;

    protected string _initialText;

    public RectTransform rectTransform;

    private void Awake()
    {
        _initialText = _text.text;
    }

    public void UpdateValue(int score)
    {
        _text.text = string.Format(_initialText, score);
    }
    
    public void UpdateValue(int score1, int score2)
    {
        _text.text = string.Format(_initialText, score1, score2);
    }
    
    public void UpdateValue(PatientJournalScreen.ScoreStruct[] data)
    {
        _text.text = string.Format(_initialText, data[0].score, data[1].score);
    }
}
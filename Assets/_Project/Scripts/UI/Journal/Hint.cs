using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] protected Text _text;
    [SerializeField] protected Canvas _canvas;

    protected string _initialText;

    public RectTransform rectTransform;

    private void Awake()
    {
        _initialText = _text.text;
    }

    public void UpdateValue(string score)
    {
        _canvas.enabled = !string.IsNullOrEmpty(score);
        
        _text.text = string.Format(_initialText, score);
    }

    public void UpdateValue(string score1, string score2)
    {
        _text.text = string.Format(_initialText, score1, score2);
    }
}
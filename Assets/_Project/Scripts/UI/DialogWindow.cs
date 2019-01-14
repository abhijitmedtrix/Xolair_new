using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoSingleton<DialogWindow>
{
    [SerializeField] private Text _titleText, _messageText;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private GameObject[] _separators;

    private static Action[] _callbacks;
    private List<Text> _buttonsTexts = new List<Text>();

    private void Awake()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            Button btn = _buttons[i];
            _buttonsTexts.Add(btn.GetComponentInChildren<Text>(true));

            int index = i;
            btn.onClick.AddListener(() =>
                OnButtonClicked(index)
            );
        }
    }

    private static void OnButtonClicked(int index)
    {
        // invoke callback if exists
        if (index < _callbacks.Length)
        {
            _callbacks[index]?.Invoke();
        }

        // close in any case
        Hide();
    }


    public static void Show(string title, string message, string[] buttonsNames, Action[] callbacks)
    {
        Instance.ShowInternal(title, message, buttonsNames, callbacks);
    }

    private void ShowInternal(string title, string message, string[] buttonsNames, Action[] callbacks)
    {
        this.gameObject.SetActive(true);
        this.transform.SetAsLastSibling();

        _callbacks = callbacks;

        _titleText.text = title;
        _messageText.text = message;

        _titleText.gameObject.SetActive(!string.IsNullOrEmpty(title));
        _messageText.gameObject.SetActive(!string.IsNullOrEmpty(message));

        int buttonsCount = buttonsNames.Length;

        for (int i = 0; i < _separators.Length; i++)
        {
            _separators[i].SetActive(i < buttonsCount);
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i < buttonsCount)
            {
                _buttonsTexts[i].text = buttonsNames[i];
                _buttons[i].gameObject.SetActive(true);
            }
            else
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
    }

    private static void Hide()
    {
        Instance.HideInternal();
    }

    private void HideInternal()
    {
        this.gameObject.SetActive(false);

        _callbacks = null;
    }
}
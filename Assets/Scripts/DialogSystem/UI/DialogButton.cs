using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _uiText;
    [SerializeField] Button _button;
    Action<string, string> _answerAction;
    string _answerText;
    string _answerTriggers;

    public void Initialize(Action<string, string> answer)
    {
        _answerAction = answer;
        _button.onClick.AddListener(Click);
    }
    public void SetActive(bool active) => gameObject.SetActive(active);
    public void TakeText(string text, string triggers)
    {
        _uiText.text = "   " + text;
        _answerText = text;
        _answerTriggers = triggers;
    }
    void Click() => _answerAction.Invoke(_answerText, _answerTriggers);
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] GameObject _panelPlayer;
    [SerializeField] List<DialogButton> _buttons;

    [SerializeField] GameObject _panelNpc;
    [SerializeField] TextMeshProUGUI _uiTextNpc;

    Action<string> _nextKnot;
    public void Initialize(Action<string> nextKnot)
    {
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i].Initialize(TakeAnswer);
        _panelPlayer.gameObject.SetActive(false);
        _nextKnot = nextKnot;
    }

    public void TurnOnPanel(List<string> answer, List<string> triggers)
    {
        _panelPlayer.gameObject.SetActive(true);
        for (int i = 0; i < answer.Count; i++)
        {
            _buttons[i].SetActive(true);
            _buttons[i].TakeText(answer[i], triggers[i]);
        }
    }

    void TakeAnswer(string text, string triggers)
    {
        _panelPlayer.gameObject.SetActive(false);
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i].SetActive(false);
        string part = text + "\t" + triggers;
        _nextKnot.Invoke(part);
    }

    public void ShowText(string text)
    {
        _panelNpc.SetActive(true);
        _uiTextNpc.text = text;
    }
    public void PanelOff() => _panelNpc.SetActive(false);
}

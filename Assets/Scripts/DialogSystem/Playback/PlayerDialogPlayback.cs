using System;
using UnityEngine;

public class PlayerDialogPlayback : MonoBehaviour, IDialogAction
{
    [SerializeField] UniversalAnimated UniversalAnimated;
    [SerializeField] UniversalAudio UniversalAudio;

    Action _startTrade;
    public void Initialize(Action startTrade)
    {
        _startTrade = startTrade;
    }
    public void StartAnimation(string name) => UniversalAnimated.StartAnimation(name);
    public void StartAudio(string name) => UniversalAudio.StartAudio(name);
    public void ChangeState(string state)
    {
        if(state == "торговля")
        {
            _startTrade.Invoke();
        }
    }
}

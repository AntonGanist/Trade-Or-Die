using System.Collections.Generic;
using UnityEngine;

public class NpcDialogPlayback : MonoBehaviour, IDialogAction 
{ 
    [SerializeField] UniversalAnimated _universalAnimated;
    [SerializeField] UniversalAudio _universalAudio;
    string _name;
    List<string> _startKnots;
    public string GetName() => _name;
    public string GetKnot()
    {
        int i = Random.Range(0, _startKnots.Count);
        return _startKnots[i];
    }
    public void TakeDialogData(DialogData dialogData)
    {
        _name = dialogData.Name;
        _startKnots = dialogData.StartKnots;
    }
    public void ClearData()
    {
        _startKnots = null;
        _name = null;
    }
    public bool NpcIsHere() => _name != null;

    public void StartAnimation(string name) => _universalAnimated.StartAnimation(name);
    public void StartAudio(string name) => _universalAudio.StartAudio(name);

    public void ChangeState(string state)
    {

    }

}
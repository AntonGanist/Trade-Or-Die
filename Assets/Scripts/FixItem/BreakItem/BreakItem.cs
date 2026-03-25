using System;
using System.Collections.Generic;
using UnityEngine;

public class BreakItem : MonoBehaviour
{
    [SerializeField] Screw[] _screws;
    [SerializeField] MetalPlate[] _metalPlates;
    [SerializeField] DustyArea _dustyArea;
    List<ScrewHole> _screwHoles = new();
    public void Initialize(Action<Transform> unscrew, Action screwed)
    {
        for(int i = 0; i< _screws.Length; i++)
            _screws[i].Initialize(unscrew, screwed);
        for(int i = 0; i< _metalPlates.Length; i++)
        {
            _metalPlates[i].Initialize();
            _screwHoles.AddRange(_metalPlates[i].GetHoles());
        }
        _dustyArea.Initialize();
    }
    public ScrewHole GetHole()
    {
        ScrewHole screwHole = null;
        for (int i = 0; i< _screwHoles.Count; i++)
        {
            if (!_screwHoles[i].HasScrew() && !_screwHoles[i].PlateOpen())
            {
                screwHole = _screwHoles[i];
                break;
            }
        }
        return screwHole;
    }
    public bool CheckRepair()
    {
        for (int i = 0; i< _metalPlates.Length; i++)
        {
            if (_metalPlates[i].IsOpen())
                return false;
        }
        if(!_dustyArea.Purely())
            return false;
        for (int i = 0; i < _screws.Length; i++)
        {
            if (_screws[i].Screwed())
                return false;
        }
        return true;
    }
}

using System;
using UnityEngine;

public class BreakItemManager : MonoBehaviour
{
    [SerializeField] BreakItem[] _breakItems;
    [SerializeField] MoveDetals _moveDetals;
    Action _unscrew;

    ScrewHole _currentHole;
    Screw _currentScrew;
    Action _endMoveScrew;
                            public BreakItem breakItem;
    public void Initialize(Action unscrew, Action endMoveScrew, Action screwed)
    {
        _unscrew = unscrew;
        _endMoveScrew = endMoveScrew;
        for (int i = 0; i < _breakItems.Length; i++)
            _breakItems[i].Initialize(MoveDetal, screwed);
        _moveDetals.Initialize(ScrewInPlace);
    }
    void MoveDetal(Transform detal)
    {
        _moveDetals.TakeDetal(detal);
        _unscrew.Invoke();
    }

    public void BackScrew(Screw screw)
    {        
        if (_currentScrew != null) return;
        _currentScrew = screw;
        _currentHole = breakItem.GetHole();
        if(_currentHole == null) return;
        _moveDetals.PutBackInPlace(_currentScrew.transform, _currentHole.GetPoint());
    }
    void ScrewInPlace()
    {
        _currentScrew.TakeHole(_currentHole);
        _currentHole.TakeScrew(_currentScrew);
        _currentScrew.transform.parent = _currentHole.transform;
        _currentScrew = null;
        _currentHole = null;
        _endMoveScrew.Invoke();
    }
    public bool CheckRepair() => breakItem.CheckRepair();
}

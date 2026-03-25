using System.Collections.Generic;
using UnityEngine;

public class MetalPlate : MonoBehaviour
{
    [SerializeField] UniversalAnimated _animated;
    [SerializeField] List<ScrewHole> _screwHoles;
    bool _open;
    public void Initialize()
    {
        for (int i = 0; i < _screwHoles.Count; i++)
            _screwHoles[i].Initialize(IsOpen);
    }
    bool CheckBracing()
    {
        int o = 0;
        for(int i = 0;  i < _screwHoles.Count; i++)
        {
            if (!_screwHoles[i].HasScrew())
                o++;
        }
        return o == _screwHoles.Count;
    }
    public void OpenOrClose()
    {
        if (!_open && CheckBracing())
        {
            _animated.StartAnimation("открыть");
            _open = true;
        }
        else if (_open)
        {
            _animated.StartAnimation("закрыть");
            _open = false;
        }
    }
    public List<ScrewHole> GetHoles() => _screwHoles;
    public bool IsOpen() => _open;
}

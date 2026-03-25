using System;
using UnityEngine;

public class ScrewHole : MonoBehaviour
{
    [SerializeField] Screw _screw;
    [SerializeField] Transform _endPoint;
    Func<bool> _plateOpen;
    public void Initialize(Func<bool> plateOpen)
    {
        _plateOpen = plateOpen;
    }
    public void TakeScrew(Screw screw) => _screw = screw;
    public bool HasScrew() => _screw != null;
    public Transform GetPoint() => _endPoint;
    public bool PlateOpen() => _plateOpen.Invoke();
}
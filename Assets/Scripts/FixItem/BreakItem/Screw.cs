using System;
using UnityEngine;

public class Screw : MonoBehaviour
{
    [SerializeField] Transform _point;
    [SerializeField] float _offset;
    [SerializeField] ScrewHole _screwHole;
    bool _wasUnscrewed;
    bool _block;
    float _startY;
    Vector3 _startPos;
    Action<Transform> _unscrew;
    Action _screwed;
    public void Initialize(Action<Transform> unscrew, Action screwed)
    {
        _startY = transform.localPosition.y;
        _startPos = transform.localPosition;
        _unscrew = unscrew;
        _screwed = screwed;
        _screwHole.TakeScrew(this);
    }
    public Transform GetPoint() => _point;
    public bool BlockMove() => _block;
    public float GetYPosition() => transform.localPosition.y;
    public void TakeNewYPosition(float y)
    {
        _startPos.y = y;
        transform.localPosition = _startPos;
        if(y > _offset)
        {
            _startPos.y = _offset;
            transform.localPosition = _startPos;
            _screwHole.TakeScrew(null);
            _screwHole = null;
            _unscrew.Invoke(transform);
            _block = true;
            _wasUnscrewed = true;
        }
        else if(y < _startY)
        {
            _startPos.y = _startY;
            transform.localPosition = _startPos;
            _block = true;
            if (_wasUnscrewed)
            {
                _screwed.Invoke();
                _wasUnscrewed = false;
            }
        }
        else
            _block = false;
    }
    public void TakeHole(ScrewHole screwHole) => _screwHole = screwHole;
    public bool HasHole() => _screwHole != null;
    public bool Screwed() => _wasUnscrewed;
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screwdriver : MonoBehaviour, ITool
{
    [SerializeField] MoveTool _moveTool;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _speed;
    Transform _parent;
    bool _blockPosition;

    bool _startFix;
    bool _onPosition = false;
    Screw _screw;

    public void Initialize(Camera camera, LayerMask layerMask)
    {
        _moveTool.Initialize(OnPosition);
        _parent = transform.parent;
    }


    public Transform GetTransform() => transform;
    public bool BlockChangePosition() => _blockPosition;
    public void BlockPosition(bool block) => _blockPosition = block;

    public void StartFix(bool start)
    {
        _startFix = start;
    }
    public void TakeScrew(Screw screw, Action adblockAction = null)
    {
        _screw = screw;
        if (screw != null)
        {
            transform.parent = screw.GetPoint();
            _moveTool.StartMoving(screw.GetPoint(), adblockAction);
        }
        else
        {
            transform.parent = _parent;
            _moveTool.StopMove();
            _onPosition = false ;
        }
    }
    public bool ToolInWork() => _screw != null;

    public void BackMove()
    {
        _moveTool.MoveInStartPosition();
        transform.parent = _parent;
        _startFix = false;
        _screw = null;
    }

    public bool ToolFix() => _startFix;
    void OnPosition() => _onPosition = !_onPosition;

    void Update()
    {
        if (Mouse.current == null || !_startFix || !_onPosition)
            return;

        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            Rotate(scroll);
            MoveAlongAxis(scroll);
        }
    }
    void Rotate(float scroll)
    {
        if (_screw.BlockMove()) return;
        Vector3 localEuler = transform.localEulerAngles;
        float newY = localEuler.y + scroll * _rotationSpeed;
        transform.localEulerAngles = new Vector3(0f, newY, 0f);
    }
    void MoveAlongAxis(float scroll)
    {
        float move = scroll * _speed;
        float newY = _screw.GetYPosition() + move;
        _screw.TakeNewYPosition(newY);
    }
}

using System;
using System.Collections;
using UnityEngine;

public class MoveTool : ChangeObjectPosition
{
    [SerializeField] Transform _target;

    Action _endMove;

    public void Initialize(Action endMove)
    {
        _endMove = endMove;
    }
    public void StopMove() => StopAllCoroutines();
    public void MoveInStartPosition() => StartCoroutine(MoveCamera(_target.position, _target.rotation));
    public void StartMoving(Transform point, Action adblockAction)
    {
        StartCoroutine(MoveCamera(point.position, point.rotation, adblockAction));
    }
    IEnumerator MoveCamera(Vector3 endPos, Quaternion endRot, Action adblockAction = null)
    {
        yield return StartCoroutine(MoveToTarget(endPos, endRot));
        _endMove.Invoke();
        adblockAction?.Invoke();
    }
}

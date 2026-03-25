using System;
using System.Collections;
using UnityEngine;

public class TakeTool : ChangeObjectPosition
{
    [SerializeField] Transform _arm;
    ITool _tool;

    public void ChangeToolPosition(Vector3 endPos, Quaternion endRot, ITool tool, Action end = null)
    {
        if (_cooldown || tool.BlockChangePosition()) return;

        _tool = tool;
        _object = _tool.GetTransform();
        if (endPos == Vector3.zero || endRot == Quaternion.identity)
            StartCoroutine(MoveTool(_arm.position, _arm.rotation, end));
        else
            StartCoroutine(MoveTool(endPos, endRot, end));

        StartCoroutine(Cooldown());
    }
    IEnumerator MoveTool(Vector3 endPos, Quaternion endRot, Action end)
    {
        _tool.BlockPosition(true);
        yield return StartCoroutine(MoveToTarget(endPos, endRot));
        _tool.BlockPosition(false);
        end?.Invoke();
    }
}

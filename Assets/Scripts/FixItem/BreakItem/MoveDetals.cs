using System;
using System.Collections;
using UnityEngine;

public class MoveDetals : ChangeObjectPosition
{
    [SerializeField] PlacementDetals _placementDetals;
    Action _end;
    public void Initialize(Action end)
    {
        _end = end;
    }
    public void TakeDetal(Transform detal)
    {
        _object = detal;
        Transform point = _placementDetals.GetPoint();
        _placementDetals.TakeDetal(detal);
        StartCoroutine(MoveToTarget(point.position, point.rotation));
    }
    public void PutBackInPlace(Transform detal, Transform point)
    {
        StartCoroutine(PutBackInPlaceCorutine(detal, point));
    }
    IEnumerator PutBackInPlaceCorutine(Transform detal, Transform point)
    {
        detal.transform.parent = null;
        _object = detal;
        yield return StartCoroutine(MoveToTargetLocal(point.position, point.rotation));
        _end.Invoke();
    }
}

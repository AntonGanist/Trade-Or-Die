using System;
using System.Collections;
using UnityEngine;


public class MovingCamera : ChangeObjectPosition
{
    [SerializeField] Transform _target;

    Transform _cameraPlayer;
    Action _startMoving;
    Action _endMoving;

    public void Initialize(Transform cameraPlayer, Action startDialog, Action endDialog)
    {
        _cameraPlayer = cameraPlayer;
        _startMoving = startDialog;
        _endMoving = endDialog;
    }
    public void StartMoving(bool start)
    {
        _object.gameObject.SetActive(true);
        Transform target;
        if (start)
        {
            target = _target;
            _object.position = _cameraPlayer.position;
            _object.rotation = _cameraPlayer.rotation;
        }
        else
            target = _cameraPlayer;

        StartCoroutine(MoveCamera(target.position, target.rotation, start));
        StartCoroutine(Cooldown());
    }
    IEnumerator MoveCamera(Vector3 endPos, Quaternion endRot, bool start)
    {
        yield return StartCoroutine(MoveToTarget(endPos, endRot));
        if (start)
        {
            _startMoving.Invoke();
        }
        else
        {
            _object.gameObject.SetActive(false);
            _endMoving.Invoke();
        }
    }
    public void ChangeCameraPosition(Vector3 position) => _object.position = position;
}

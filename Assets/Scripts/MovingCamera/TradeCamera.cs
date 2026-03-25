using UnityEngine;

public class TradeCamera : ChangeObjectPosition
{
    [SerializeField] Transform _target;
    Transform _dialogCamera;
    public void Initialize(Transform dialogCamera)
    {
        _dialogCamera = dialogCamera;
    }
    public void StartTrade()
    {
        _object.gameObject.SetActive(true);
        Transform target;
        target = _target;
        _object.position = _dialogCamera.position;
        _object.rotation = _dialogCamera.rotation;

        StartCoroutine(MoveToTarget(target.position, target.rotation));
    }
    public void CameraOff() => _object.gameObject.SetActive(false);
    public Vector3 CameraPosition() => _object.position;
}

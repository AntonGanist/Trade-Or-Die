using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    [SerializeField] float _speed;
    [SerializeField] bool _unlockX;
    [SerializeField] bool _unlockY;
    [SerializeField] bool _unlockZ;

    [SerializeField] float _offsetX;
    [SerializeField] float _offsetY;
    [SerializeField] float _offsetZ;

    Vector3 _targetVector3;
    void Update()
    {
        if (_targetTransform == null) return;
        if (_speed != 0)
        {
            Vector3 direction;
            if (_targetTransform != null)
                direction = _targetTransform.position - transform.position;
            else
                direction = _targetVector3 - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Vector3 offsetRotation = new Vector3(_offsetX, _offsetY, _offsetZ);

            Vector3 currentRotation = transform.rotation.eulerAngles + offsetRotation;

            if (_unlockX) currentRotation.x = targetRotation.eulerAngles.x;
            if (_unlockY) currentRotation.y = targetRotation.eulerAngles.y;
            if (_unlockZ) currentRotation.z = targetRotation.eulerAngles.z;

            Quaternion newRotation = Quaternion.Euler(currentRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _speed * Time.deltaTime);
        }
        else
        {
            if (_targetTransform != null)
                transform.LookAt(_targetTransform.position);
            else
                transform.LookAt(_targetVector3);
        }
    }
    public void TakeTarget(Transform target)
    {
        _targetTransform = target;
        _targetVector3 = Vector3.zero;
    }
    public void TakeTarget(Vector3 target)
    {
        _targetTransform = null;
        _targetVector3 = target;
    }
}


using UnityEngine;
using UnityEngine.InputSystem;

public class RotateAnObject : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    Camera _cam;
    [SerializeField] Transform _object;
    bool _canRotate;

    public void Initialize(Camera cam)
    {
        _cam = cam;
    }
    void Update()
    {
        if (Mouse.current == null || !_canRotate)
            return;

        if (!Mouse.current.leftButton.isPressed)
        {
            _canRotate = false;
            return;
        }

        Vector2 delta = Mouse.current.delta.ReadValue();

        float mouseX = delta.x * _rotationSpeed * Time.deltaTime;
        float mouseY = delta.y * _rotationSpeed * Time.deltaTime;

        _object.Rotate(Vector3.up, -mouseX, Space.World);
        _object.Rotate(_cam.transform.right, mouseY, Space.World);
    }

    public void StartRotate(Transform obj)
    {
        _object = obj;
        _canRotate = true;
    }

    public void ResetObj()
    {
        _object.rotation = Quaternion.identity;
    }
}
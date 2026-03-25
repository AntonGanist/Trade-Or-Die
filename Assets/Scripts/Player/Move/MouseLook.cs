using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public enum RorationAxes
    {
        XandY,
        X,
        Y
    }

    public RorationAxes _axes = RorationAxes.XandY;
    public float _rotationSpeedHor = 100f;
    public float _rotationSpeedVer = 100f;

    public float maxVert = 45f;
    public float minVert = -45f;

    private float _rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * _rotationSpeedHor * Time.deltaTime;
        float mouseY = mouseDelta.y * _rotationSpeedVer * Time.deltaTime;

        if (_axes == RorationAxes.XandY)
        {
            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            float rotationY = transform.localEulerAngles.y + mouseX;

            transform.localEulerAngles = new Vector3(_rotationX,rotationY,transform.localEulerAngles.z);
        }
        else if (_axes == RorationAxes.X)
        {
            transform.Rotate(0f, mouseX, 0f);
        }
        else if (_axes == RorationAxes.Y)
        {
            _rotationX -= mouseY;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            Vector3 worldEuler = transform.eulerAngles;
            worldEuler.x = _rotationX;
            transform.eulerAngles = worldEuler;

        }
    }
}

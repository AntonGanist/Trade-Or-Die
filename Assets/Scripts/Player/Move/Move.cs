using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] CharacterController _controller;
    [SerializeField] float _speed = 12f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _terminalVelocity = -20f;
    [SerializeField] float _jumpHeight = 3f;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = 0.4f;
    [SerializeField] LayerMask _groundMask;

    Vector3 _velocity;
    Vector3 _move;
    bool _isGrounded;

    bool _blockMove;

    Action<bool> _blockKick;

    float _speedMultiplier = 1f;
    Coroutine _slowDownCoroutine;

    public void Initialize(Action<bool> blockKick)
    {
        _blockKick = blockKick;
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        if (_blockMove) return;

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed) x -= 1f;
        if (Keyboard.current.dKey.isPressed) x += 1f;
        if (Keyboard.current.wKey.isPressed) z += 1f;
        if (Keyboard.current.sKey.isPressed) z -= 1f;

        _move = (transform.right * x + transform.forward * z).normalized;
        _controller.Move(_move * _speed * Time.deltaTime);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

        _velocity.y += _gravity * Time.deltaTime;
        if (_velocity.y < _terminalVelocity)
            _velocity.y = _terminalVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void Ride(float rideSpeed, float time)
    {
        if (_blockMove) return;

        _blockKick.Invoke(true);

        Vector3 rideDirection = _move;
        if (rideDirection == Vector3.zero)
            rideDirection = transform.forward; 

        StartCoroutine(RideCoroutine(rideDirection.normalized, rideSpeed, time));
    }
    public void Ride(float rideSpeed, float time, Vector3 rideDirection)
    {
        if (_blockMove) return;

        _blockKick?.Invoke(true);

        if (_slowDownCoroutine != null)
            StopCoroutine(_slowDownCoroutine);

        _slowDownCoroutine = StartCoroutine(RideCoroutine(rideDirection.normalized, rideSpeed, time));
    }
    IEnumerator RideCoroutine(Vector3 direction, float rideSpeed, float time)
    {
        _blockMove = true;

        float timer = 0f;
        float slowDownStartTime = time * 0.99f; 

        while (timer < time)
        {
            if (timer >= slowDownStartTime)
            {
                float slowTimer = (timer - slowDownStartTime) / (time - slowDownStartTime);
                _speedMultiplier = Mathf.Lerp(1f, 0f, slowTimer);
            }
            else
                _speedMultiplier = 1f;

            _controller.Move(direction * rideSpeed * _speedMultiplier * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        _speedMultiplier = 1f;
        _blockKick?.Invoke(false);
        _blockMove = false;
    }

    public bool GetGrounded() => _isGrounded;

    public void TakeSpeed(float speed) => _speed = speed;
    public float GetSpeed() => _speed;
}

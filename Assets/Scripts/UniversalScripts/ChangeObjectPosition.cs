using System.Collections;
using UnityEngine;

public class ChangeObjectPosition : MonoBehaviour
{
    [SerializeField] protected Transform _object;
    [SerializeField] float _speed;
    [SerializeField] float _interactCooldown;
    protected bool _cooldown;

    protected IEnumerator MoveToTarget(Vector3 endPos, Quaternion endRot)
    {
        Vector3 startPos = _object.position;
        Quaternion startRot = _object.rotation;

        float distance = Vector3.Distance(startPos, endPos);

        float duration = distance / _speed;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float smoothT = 1f - Mathf.Pow(1f - t, 3f);

            _object.position = Vector3.Lerp(startPos, endPos, smoothT);
            _object.rotation = Quaternion.Slerp(startRot, endRot, smoothT);

            yield return null;
        }
        _object.position = endPos;
        _object.rotation = endRot;
    }
    protected IEnumerator MoveToTargetLocal(Vector3 endPos, Quaternion endRot)
    {
        Vector3 startPos = _object.localPosition;
        Quaternion startRot = _object.rotation;

        float distance = Vector3.Distance(startPos, endPos);

        float duration = distance / _speed;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float smoothT = 1f - Mathf.Pow(1f - t, 3f);

            _object.localPosition = Vector3.Lerp(startPos, endPos, smoothT);
            _object.rotation = Quaternion.Slerp(startRot, endRot, smoothT);

            yield return null;
        }
        _object.localPosition = endPos;
        _object.rotation = endRot;
    }
    protected IEnumerator Cooldown()
    {
        if (_interactCooldown != 0)
        {
            _cooldown = true;
            yield return new WaitForSeconds(_interactCooldown);
            _cooldown = false;
        }
    }
    public bool GetCooldown() => _cooldown;
}

using System;
using System.Collections;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    [SerializeField] UniversalAnimated _universalAnimated;
    [SerializeField] float _speed;

    Coroutine _moveCoroutine;


    public void Subscribe(string name)
    {
        _universalAnimated.StartAnimation(name);
    }
    public void Subscribe(string name, Action action)
    {
        _universalAnimated.StartAnimation(name, action);
    }
    public void TakePosition(float y)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveY(y));
    }

    IEnumerator MoveY(float targetY)
    {
        Vector3 pos = transform.localPosition;
        while (Mathf.Abs(pos.y - targetY) > 0.01f)
        {
            pos = transform.localPosition;
            float newY = Mathf.MoveTowards(pos.y, targetY, _speed * Time.deltaTime);
            transform.localPosition = new Vector3(pos.x, newY, pos.z);
            yield return null;
        }

        pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, targetY, pos.z);

        _moveCoroutine = null;
    }
}
using System;
using System.Collections;
using UnityEngine;

public class ComingNpc : MonoBehaviour
{
    [SerializeField] float _comingTime;
    Action _npcHere;
    public void Initialize(Action npcHere)
    {
        _npcHere = npcHere;
        StartCoroutine(ComingNpcCorutine());
    }
    public void StartExpectation() => StartCoroutine(ComingNpcCorutine());
    IEnumerator ComingNpcCorutine()
    {
        yield return new WaitForSeconds(_comingTime);
        _npcHere.Invoke();
    }
}

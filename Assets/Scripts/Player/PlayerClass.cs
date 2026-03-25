using System;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] Move _move;
    [SerializeField] MouseLook[] _mouseLooks;

    [SerializeField] PlayerDialogPlayback _playerDialogPlayback;
    public void Initialize(Action startTrade)
    {
        _playerDialogPlayback.Initialize(startTrade);
    }
    public MouseLook[] GetMouseLooks() => _mouseLooks;
    public Move GetMove() => _move;
    public PlayerDialogPlayback GetDialogPlayback() => _playerDialogPlayback;
}

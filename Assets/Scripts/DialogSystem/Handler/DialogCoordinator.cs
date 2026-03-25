using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogCoordinator : MonoBehaviour
{
    [SerializeField] DialogPanel _panel;
    MovingCamera _camera;

    DialogRepository _repository;
    DialogFlowController _flow;
    DialogPlayback _playback;

    PlayerDialogPlayback _player;

    Action<bool> _setDialogState;
    public void Initialize(PlayerDialogPlayback playerDialogPlayback, Action<bool> setDialogState, MovingCamera camera)
    {
        _player = playerDialogPlayback;
        _setDialogState = setDialogState;
        _camera = camera;

        _repository = new DialogRepository();
        _flow = new DialogFlowController();

        _panel.Initialize(OnAnswerSelected);

        _playback = new DialogPlayback(this, _panel, null);

        _playback.OnAutoNextRequested += HandleAutoNext;
        _playback.OnNodeFinished += HandleNodeFinished;
    }

    public void StartDialog(NpcDialogPlayback npc)
    {
        _flow.SetCurrentNpc(npc);
        _flow.Start(npc.GetKnot());

        _camera.StartMoving(true);

        _repository.Load(npc.GetName());
    }

    public void StopDialog()
    {
        _panel.PanelOff();
        _camera.StartMoving(false);
        _playback.Stop();
        _flow.Reset();
    }
    public void EndDialog()
    {
        _setDialogState.Invoke(false);
    }

    public void PlayCurrentKnot()
    {
        string npcName = _flow.CurrentNpc.GetName();
        string knot = _flow.CurrentKnot;

        if (_flow.IsExit(knot))
        {
            StopDialog();
            return;
        }

        if (!_repository.Has(npcName, knot))
        {
            Debug.LogError($"ƒиалог не найден: {npcName} / {knot}");
            return;
        }

        var executor = new DialogTriggerExecutor(_flow.CurrentNpc, _player);

        _playback = new DialogPlayback(this, _panel, executor);

        _playback.OnAutoNextRequested += HandleAutoNext;
        _playback.OnNodeFinished += HandleNodeFinished;

        List<string> dialog = _repository.GetDialog(npcName, knot);
        List<string> options = _repository.GetOptions(npcName, knot);

        _playback.PlayNode(dialog, options);
    }

    void HandleAutoNext(string nextKnot)
    {
        _flow.SetCurrentKnot(nextKnot);
        PlayCurrentKnot();
    }

    void HandleNodeFinished()
    {
        // ”зел закончилс€ без autoNext.
        // ∆дЄм выбор игрока (если есть варианты).
    }

    void OnAnswerSelected(string metaData)
    {
        _playback.TryParseLine(metaData);

        string[] parts = metaData.Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 0)
            return;

        string nextKnot = parts[parts.Length-1];

        _flow.SetCurrentKnot(nextKnot);
        PlayCurrentKnot();
    }

    public void PanelOff() => _panel.PanelOff();
    public void ChangeCameraPosition(Vector3 position) => _camera.ChangeCameraPosition(position);
}
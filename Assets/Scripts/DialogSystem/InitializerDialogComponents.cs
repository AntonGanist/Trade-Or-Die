using UnityEngine;

public class InitializerDialogComponents : MonoBehaviour
{
    [SerializeField] DialogCoordinator _dialogCoordinator;
    [SerializeField] DialogRunner _dialogRunner;
    [SerializeField] MovingCamera _dialogCamera;

    public void Initialize(MouseLook[] mouseLooks, Move move, PlayerDialogPlayback playerDialogPlayback)
    {
        _dialogCoordinator.Initialize(playerDialogPlayback, _dialogRunner.SetDialogState, _dialogCamera);

        _dialogRunner.Initialize(mouseLooks, move, _dialogCoordinator);
        _dialogCamera.Initialize(mouseLooks[0].transform, _dialogCoordinator.PlayCurrentKnot, _dialogCoordinator.EndDialog);
    }

    public void PanelOff() => _dialogCoordinator.PanelOff();
    public void StopDialog() => _dialogCoordinator.StopDialog();
    public void ChangeCameraPosition(Vector3 position) => _dialogCoordinator.ChangeCameraPosition(position);
}


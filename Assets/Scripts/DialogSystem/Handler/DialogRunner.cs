using UnityEngine;
using UnityEngine.InputSystem;

public class DialogRunner : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] float _distance;

    [SerializeField] Key _interactKey;

    DialogCoordinator _dialogCoordinator;

    MouseLook _mouseLookCamera;
    MouseLook _mouseLookPlayer;
    Move _move;

    bool _dialogueGoingOn;

    public void Initialize(MouseLook[] mouseLooks, Move move, DialogCoordinator dialogCoordinator)
    {
        _mouseLookCamera = mouseLooks[0];
        _mouseLookPlayer = mouseLooks[1];
        _move = move;
        _dialogCoordinator = dialogCoordinator;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current[_interactKey].wasPressedThisFrame && !_dialogueGoingOn)
        {
            Ray ray = new Ray(_camera.position, _camera.forward);

            if (!Physics.Raycast(ray, out RaycastHit hit, _distance))
                return;

            NpcDialogPlayback npc = hit.collider.GetComponent<NpcDialogPlayback>();

            if (npc != null && npc.NpcIsHere())
            {
                _dialogCoordinator.StartDialog(npc);
                SetDialogState(true);
            }
        }
    }

    public void SetDialogState(bool state)
    {
        _dialogueGoingOn = state;

        _mouseLookCamera.gameObject.SetActive(!state);
        _mouseLookPlayer.enabled = !state;
        _move.enabled = !state;

        if (state) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
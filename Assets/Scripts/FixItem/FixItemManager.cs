using UnityEngine;
using UnityEngine.InputSystem;

public class FixItemManager : MonoBehaviour
{
    [SerializeField] Key _interactKey;
    [SerializeField] MovingCamera _movingCamera;

    [SerializeField] RotateAnObject _rotateAnObject;
    [SerializeField] TakeItemFromTable _takeItemFromTable;
    [SerializeField] ToolManager _toolManager;

    [SerializeField] BreakItemManager _breakItemManager;

    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _layerMask;

    bool _workIsDone;

    bool _playerEnterArea;
    bool _playerFix;

    MouseLook[] _mouseLooks;
    Move _move;

    public void Initialize(MouseLook[] mouseLooks, Move move)
    {
        _mouseLooks = mouseLooks;
        _move = move;

        _toolManager.Initialize(_camera, _layerMask);

        _movingCamera.Initialize(mouseLooks[0].transform, PlayerMoveComponents, PlayerMoveComponents);
        _takeItemFromTable.Initialize(_rotateAnObject, _layerMask, _camera, _breakItemManager.BackScrew, CheckRepair);

        _breakItemManager.Initialize(_takeItemFromTable.ToolInHand, _takeItemFromTable.BlockAction,
            _takeItemFromTable.ToolInHand);
    }
    void Update()
    {
        if (Keyboard.current == null || !_playerEnterArea || _movingCamera.GetCooldown() || _workIsDone)
            return;

        if (Keyboard.current[_interactKey].wasPressedThisFrame)
        {
            _playerFix = !_playerFix;
            if (_playerFix)
            {
                PlayerMoveComponents();
            }
            _movingCamera.StartMoving(_playerFix);

        }
    }

    void CheckRepair()
    {
        if (!_breakItemManager.CheckRepair()) return;
        _playerFix = !_playerFix;
        _movingCamera.StartMoving(false);
        _workIsDone = true;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerClass playerClass = other.GetComponent<PlayerClass>();
        if (playerClass != null)
            _playerEnterArea = true;
    }
    void OnTriggerExit(Collider other)
    {
        PlayerClass playerClass = other.GetComponent<PlayerClass>();
        if (playerClass != null)
            _playerEnterArea = false;
    }

    void PlayerMoveComponents()
    {
        _mouseLooks[0].gameObject.SetActive(!_playerFix);
        _mouseLooks[0].enabled = !_playerFix;
        _mouseLooks[1].enabled = !_playerFix;
        _move.enabled = !_playerFix;

        _takeItemFromTable.enabled = _playerFix;

        if (_playerFix)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _rotateAnObject.ResetObj();
        }
    }
}

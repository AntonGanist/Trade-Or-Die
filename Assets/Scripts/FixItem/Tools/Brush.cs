using UnityEngine;
using UnityEngine.InputSystem;

public class Brush : MonoBehaviour, ITool
{
    [SerializeField] MoveTool _moveTool;
    [SerializeField] Follow _follow;
    [SerializeField] Collider _collider;
    [SerializeField] float _distForDurti;
    [SerializeField] float _dist;
    [SerializeField] float _speed;

    [SerializeField] float _powerClear;
    [SerializeField] UniversalAnimated _universalAnimated;
    Transform _target;
    Camera _camera;
    LayerMask _layerMask;
    Transform _parent;
    bool _blockPosition;

    DustyArea _dustyArea;
    bool _startFix;
    bool _onPosition = false;
    public void Initialize(Camera camera, LayerMask layerMask)
    {
        _moveTool.Initialize(OnPosition);
        _parent = transform.parent;
        _camera = camera;
        _layerMask = layerMask;
        _target = _follow.transform;
        _target.parent = _parent;
    }


    public Transform GetTransform() => transform;
    public bool BlockChangePosition() => _blockPosition;
    public void BlockPosition(bool block) => _blockPosition = block;

    public void StartFix(bool start)
    {
        _startFix = start;
        _collider.enabled = !_startFix;
    }

    public void TakeTarget(DustyArea dustyArea)
    {
        _dustyArea = dustyArea;
        _follow.TakeTarget(_dustyArea.transform);
        _target.transform.position = transform.position;
        _target.transform.localRotation = transform.localRotation;
        _target.localRotation *= Quaternion.Euler(-90, 0f, 90);
        transform.parent = _target;
    }

    public void BackMove()
    {
        _moveTool.MoveInStartPosition();
        transform.parent = _parent;
        _startFix = false;
    }

    public bool ToolFix() => _startFix;
    void OnPosition() => _onPosition = !_onPosition;

    void Update()
    {
        if (Mouse.current == null || !_startFix)
            return;
        if (Mouse.current.leftButton.isPressed)
        {
            MoveTargetToCursor();
            ClearZone();
        }
        else
        {
            BackMove();
            _follow.TakeTarget(null);
            _startFix = false;
            _dustyArea = null;
        }

    }
    void MoveTargetToCursor()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 pos;
        if (Physics.Raycast(ray, out RaycastHit hit, _dist, _layerMask))
        {
            pos = hit.point - ray.direction * _distForDurti;
        }
        else
            pos = ray.origin + ray.direction * _dist - ray.direction* _distForDurti;

        _target.position = Vector3.Lerp(transform.position, pos, _speed * Time.deltaTime);
    }
    void ClearZone()
    {
        if (Vector3.Distance(_dustyArea.transform.position, _target.position) < _distForDurti * 1.1f)
        {
            _dustyArea.Clear(_powerClear);
            _universalAnimated.StartAnimation("чистить");
        }
    }
}

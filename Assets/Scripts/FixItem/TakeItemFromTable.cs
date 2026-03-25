using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeItemFromTable : MonoBehaviour
{
    [SerializeField] TakeTool _takeTool;

    Camera _camera;
    LayerMask _layerMask;

    RotateAnObject _rotateAnObject;
    ITool _tool;

    Action<Screw> _screwInPlace;
    bool _blockAction;
    Action _checkRepair;

    public void Initialize(RotateAnObject rotateAnObject, LayerMask layerMask, Camera camera, Action<Screw> screwInPlace,
        Action checkRepair)
    {
        _camera = camera;
        _layerMask = layerMask;
        _rotateAnObject = rotateAnObject;
        _screwInPlace = screwInPlace;
        _checkRepair = checkRepair;
        _rotateAnObject.Initialize(_camera);
    }
    void Update()
    {
        if (Mouse.current == null)
            return;

        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 999, _layerMask))
        {
            FixItem fixItem = hit.collider.GetComponent<FixItem>();
            if (fixItem != null && !_blockAction)
            {
                _rotateAnObject.StartRotate(fixItem.transform);
                return;
            }


            ToolZone toolZone = hit.collider.GetComponent<ToolZone>();
            if (toolZone != null && _tool != null)
            {
                toolZone.GetToolStartPosition(_tool.GetTransform(), out Vector3 pos, out Quaternion rot);
                _takeTool.ChangeToolPosition(pos, rot, _tool, ToolIsNull);
                return;
            }

            if(_blockAction) return;

            ITool tool = hit.collider.GetComponent<ITool>();
            if ((tool != null && _tool == null) || (tool != null && _tool != null && _tool == tool))
            {
                _tool = tool;
                _takeTool.ChangeToolPosition(Vector3.zero, Quaternion.identity, _tool);
                return;
            }
            if (_tool == tool && _tool != null && _tool.ToolFix())
            {
                ToolInHand();
                return;
            }


            Screw screw = hit.collider.GetComponent<Screw>();//есть скачки если во время когда отвертка идет в руку
            //игрок выбирает новый шуруп
            if (screw != null)
            {
                if (_tool != null && screw.HasHole() && _tool is Screwdriver screwdriver && !screwdriver.ToolInWork())
                {
                    _blockAction = true;
                    screwdriver.StartFix(true);
                    screwdriver.TakeScrew(screw, BlockAction);
                }
                else if(!screw.HasHole())
                {
                    _blockAction = true;
                    _screwInPlace.Invoke(screw);
                }
                return;
            }


            MetalPlate metalPlate = hit.collider.GetComponent<MetalPlate>();
            if(metalPlate != null)
            {
                metalPlate.OpenOrClose();
            }


            DustyArea dustyArea = hit.collider.GetComponent<DustyArea>();
            if(dustyArea != null && _tool != null && _tool is Brush brush)
            {
                brush.StartFix(true);
                brush.TakeTarget(dustyArea);
            }
        }
    }

    public void BlockAction() => _blockAction = false;

    public void ToolInHand()
    {
        _tool.BackMove();
    }

    public void ToolIsNull()
    {
        _tool.StartFix(false); 
        if (_tool is Screwdriver screwdriver)
            screwdriver.TakeScrew(null);
        _tool = null;
        _checkRepair.Invoke();
    }
}
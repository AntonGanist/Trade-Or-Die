using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> _tools;
    [SerializeField] ToolZone _toolZone;
    List<ITool> _toolInterfaces;
    public void Initialize(Camera camera, LayerMask layerMask)
    {
        _toolInterfaces = new List<ITool>();

        for (int i = 0; i < _tools.Count; i++)
        {
            ITool tool = _tools[i] as ITool;
            if (tool != null)
                _toolInterfaces.Add(tool);
        }

        foreach (var tool in _toolInterfaces)
            tool.Initialize(camera, layerMask);
        _toolZone.Initialize(_tools);
    }
}

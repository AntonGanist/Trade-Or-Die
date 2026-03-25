using System.Collections.Generic;
using UnityEngine;

public class ToolZone : MonoBehaviour
{
    List<Transform> _tools = new();
    List<Vector3> _toolsPosition = new();
    List<Quaternion> _toolsRotation = new();

    public void Initialize(List<MonoBehaviour> tools)
    {
        for(int i = 0; i < tools.Count; i++)
        {
            _tools.Add(tools[i].transform);
            _toolsPosition.Add(_tools[i].position);
            _toolsRotation.Add(_tools[i].rotation);
        }
    }
    public void GetToolStartPosition(Transform tool, out Vector3 pos, out Quaternion rot)
    {
        pos = Vector3.zero;
        rot = Quaternion.identity;
        for (int i = 0; i < _tools.Count; i++)
        {
            if (_tools[i] == tool)
            {
                pos = _toolsPosition[i];
                rot = _toolsRotation[i];
            }
                
        }
    }
}

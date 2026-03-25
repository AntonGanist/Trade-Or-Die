using UnityEngine;

public class PlacementDetals : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    public Transform GetPoint()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            if (_points[i].childCount == 0)
            {
                return _points[i];
            }
        }
        return null;
    }
    public void TakeDetal(Transform detal)
    {
        for(int i = 0; i < _points.Length; i++)
        {
            if(_points[i].childCount == 0)
            {
                detal.parent = _points[i];
                break;
            }
        }
    }
}

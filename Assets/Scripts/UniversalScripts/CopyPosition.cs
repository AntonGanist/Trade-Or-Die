using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] Transform _target;
    void Update()
    {
        if(_target != null)
            transform.position = _target.position;
    }
}

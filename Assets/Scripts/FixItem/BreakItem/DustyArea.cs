using UnityEngine;

public class DustyArea : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Vector2 _limits;
    float _durti;
    public void Initialize()
    {
        _durti = _limits.y;
        _material.SetFloat("_Blend", _durti);
    }

    public void Clear(float clear)
    {
        _durti -= clear;
        _durti = Mathf.Clamp(_durti, _limits.x, _limits.y);
        _material.SetFloat("_Blend", _durti);
    }
    public bool Purely() => _durti == _limits.x;
}

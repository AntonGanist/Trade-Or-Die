using UnityEngine;

public interface ITool
{
    public void Initialize(Camera camera, LayerMask layerMask);
    public Transform GetTransform();
    public bool BlockChangePosition();
    public void BlockPosition(bool block);
    public void StartFix(bool start);
    public void BackMove();
    public bool ToolFix();
}

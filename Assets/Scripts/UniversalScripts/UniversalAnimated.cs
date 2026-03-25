using System;
using UnityEngine;

public class UniversalAnimated : MonoBehaviour
{
    [SerializeField] Animator _animator;
    Action _onAnimationEnd;
    public void StartAnimation(string name) => _animator.SetTrigger(name);
    public void StartAnimation(string name, Action onComplete)
    {
        _onAnimationEnd = onComplete;
        _animator.SetTrigger(name);
    }

    public void AnimatorSetActive(bool isActive) => _animator.enabled = isActive;
    public void BoolAnimation(string name, bool Bool) => _animator.SetBool(name, Bool);

    public void AnimationFinished()
    {
        _onAnimationEnd?.Invoke();
        _onAnimationEnd = null;
    }
}
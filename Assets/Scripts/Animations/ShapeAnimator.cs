using UnityEngine;
using DG.Tweening;

public class ShapeAnimator : MonoBehaviour
{
    // [SerializeField] private Animator _animator;
    [SerializeField] private Transform _transform;
    // private readonly int ScaleDown = Animator.StringToHash(nameof(ScaleDown));
    
    public void PlayScaleDownAnimation()
    {
        _transform.DOScale(0, .3f).SetEase(Ease.InBack);
        // _animator.SetTrigger(ScaleDown); 
    }
}

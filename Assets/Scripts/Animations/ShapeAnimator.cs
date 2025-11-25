using UnityEngine;

public class ShapeAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private readonly int ScaleDown = Animator.StringToHash(nameof(ScaleDown));
    
    public void PlayScaleDownAnimation()
    {
        _animator.SetTrigger(ScaleDown); 
    }
}

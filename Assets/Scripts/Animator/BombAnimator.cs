using UnityEngine;

public class BombAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int Defuse = Animator.StringToHash(nameof(Defuse));
    private readonly int Idle = Animator.StringToHash(nameof(Idle));

    public void PlayDefuseAnimation()
    {
        // _animator.SetBool(Idle, false);
        _animator.SetTrigger(Defuse);
    }
}
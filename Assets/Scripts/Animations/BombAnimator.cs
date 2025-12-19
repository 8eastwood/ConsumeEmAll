using UnityEngine;

public class BombAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int Defuse = Animator.StringToHash(nameof(Defuse));

    public void PlayDefuseAnimation()
    {
        _animator.SetTrigger(Defuse);
    }
}
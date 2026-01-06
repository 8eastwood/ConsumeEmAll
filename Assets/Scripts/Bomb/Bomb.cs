using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(ColorIdentity))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private BoxCollider _physicalCollider;
    [SerializeField] private BombAnimator _bombAnimator;
    [SerializeField] private Animator _animator;

    private ColorIdentity _colorIdentity;
    private Outline _outline;

    public bool IsCollected = false;
    public ColorType Color => _colorIdentity.Color;

    private void Awake()
    {
        _colorIdentity = GetComponent<ColorIdentity>();
        _outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        // SetOutlineOn();
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Idle"))
        {
            _physicalCollider.enabled = false;
        }

        if (stateInfo.IsName("Defuse") && stateInfo.normalizedTime >= 1 && !_animator.IsInTransition(0))
        {
            Destroy(gameObject);
        }
    }

    public void PlayDefuseAnimation()
    {
        _bombAnimator.PlayDefuseAnimation();
    }

    public void ChangeCollectedState()
    {
        IsCollected = true;
    }

    public void SetOutlineOn()
    {
        _outline.OutlineWidth = 10;
    }

    public void SetOutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}
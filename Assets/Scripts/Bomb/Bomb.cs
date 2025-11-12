using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _physicalCollider;

    private Outline _outline;

    private void OnEnable()
    {
        _outline = GetComponent<Outline>();
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

    public void SetOutlineOn()
    {
        _outline.OutlineWidth = 10;
    }

    public void SetOutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}
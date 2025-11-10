using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider _physicalCollider;
    
    // public void Remove()
    // {
    //     Destroy(gameObject);
    // }

    private void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (!stateInfo.IsName("Idle"))
        {
            _physicalCollider.enabled = false;
        }
    }
}

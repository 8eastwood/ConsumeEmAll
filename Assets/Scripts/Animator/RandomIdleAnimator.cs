using System.Collections;
using UnityEngine;

public class RandomIdleAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private string stateName = "IdleBlendTree";
    [SerializeField] private string parameterName = "IdleIndex";
    [SerializeField] private int clipCount = 4;
    [SerializeField] private float minWaitingTime = 3f;
    [SerializeField] private float maxWaitingTime = 10f;

    private void OnEnable()
    {
        StartCoroutine(ChangeIdleAnimation());
    }
    
    private IEnumerator ChangeIdleAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitingTime, maxWaitingTime));
            int index = Random.Range(0, clipCount);
            animator.SetFloat(parameterName, index);
            animator.Play(stateName, 0, Random.value);
            animator.Update(0);
        }
    }

    // private void Start()
    // {
    //     int index = Random.Range(0, clipCount);
    //     
    //     animator.SetFloat(parameterName, index);
    //     animator.Play(stateName, 0, Random.value);
    //     animator.Update(0);
    // }
}
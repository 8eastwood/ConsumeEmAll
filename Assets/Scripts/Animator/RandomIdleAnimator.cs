using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<string> _idleAnimations;
    [SerializeField] private float _minWaitingTime = 10f;
    [SerializeField] private float _maxWaitingTime = 20f;

    private float _timer;

    private void Awake()
    {
        _idleAnimations =
            new List<string> { "HappyIdle", "SadIdle", "DwarfIdle", "BreathingIdle" };
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Idle"))
        {
            _timer += Time.deltaTime;
            
            if (_timer >= GetTimeUntilNextAnimation())
            {
                PlayRandomIdleAnimation();
                ResetTimer();
            }
        }
    }

    private float GetTimeUntilNextAnimation()
    {
        return Random.Range(_minWaitingTime, _maxWaitingTime);
    }

    private void ResetTimer()
    {
        _timer = 0;
    }

    private void PlayRandomIdleAnimation()
    {
        string randomIdleAnimation = _idleAnimations[Random.Range(0, _idleAnimations.Count)];
        _animator.SetTrigger(randomIdleAnimation);
    }
}
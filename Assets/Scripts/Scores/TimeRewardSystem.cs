using UnityEngine;
using UnityEngine.Serialization;

public class TimeRewardSystem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Timer _timer;
    [Header("Rewards")]
    [SerializeField] private int _goldenRewardAmount;
    [SerializeField] private int _silverRewardAmount;
    [SerializeField] private int _bronzeRewardAmount;
    [SerializeField] private int _defaultRewardAmount;
    [Header("Time Frames")]
    [SerializeField] private float _goldTime;
    [SerializeField] private float _silverTime;
    [SerializeField] private float _bronzeTime;
    
    private float _finalReward;

    public int CalculateReward()
    {
        float elapsedTime = _timer.LevelStartTime - _timer.TimeLeft;

        if (elapsedTime <= _goldTime)
            return _goldenRewardAmount;
        
        else if (elapsedTime <= _silverTime)
            return _silverRewardAmount;
        
        else if (elapsedTime <= _bronzeTime)
            return _bronzeRewardAmount;

        else
            return _defaultRewardAmount;
    }
}
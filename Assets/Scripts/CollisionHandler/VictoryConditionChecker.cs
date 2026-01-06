using System;
using UnityEngine;

public class VictoryConditionChecker : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private int _winValue;

    private int _value;

    public event Action LevelComplete;

    public void ChangeCollectedValue()
    {
        _value++;
        CheckWinCondition(_value);
    }

    private void CheckWinCondition(int value)
    {
        if (value >= _winValue)
            LevelComplete?.Invoke();
    }
}
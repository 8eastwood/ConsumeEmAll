using System;
using UnityEngine;

public class VictoryConditionChecker : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private CollisionHandler[] _collisionHandlers;

    [Header("Values")] [SerializeField] private int _winValue;

    private int _value = 0;

    public event Action LevelComplete;

    private void OnEnable()
    {
        foreach (var collisionHandler in _collisionHandlers)
        {
            collisionHandler.UnitReached += OnBombCollected;
        }
    }

    private void OnDisable()
    {
        foreach (var collisionHandler in _collisionHandlers)
        {
            collisionHandler.UnitReached -= OnBombCollected;
        }
    }

    private void OnBombCollected(Bomb bomb)
    {
        if (bomb.IsCollected == false)
        {
            bomb.ChangeCollectedState();
            _value++;
            Debug.Log(_value);
            CheckWinCondition(_value);
        }
    }

    private void CheckWinCondition(int value)
    {
        if (value >= _winValue)
            LevelComplete?.Invoke();
    }
}
using System;
using UnityEngine;

public abstract class BaseAbilityTokenResource : MonoBehaviour, IAbilityTokenResource
{
    [SerializeField] private int _initialAmount;

    private int _currentAmount;

    public int CurrentAmount => _currentAmount;
    
    public event Action<int> AmountChanged;

    protected virtual void Start()
    {
        ResetToInitial();
    }

    public bool TryConsume() 
    {
        if (_currentAmount > 0)
        {
            _currentAmount--;
            OnAmountChanged();

            return true;
        }

        return false;
    }

    public void AddTokens(int amount)
    {
        if (amount > 0)
        {
            _currentAmount += amount;
            OnAmountChanged();
        }
    }

    public void ResetToInitial()
    {
        _currentAmount = _initialAmount;
        OnAmountChanged();
    }

    protected void OnAmountChanged()
    {
        AmountChanged?.Invoke(_currentAmount);
    }
}
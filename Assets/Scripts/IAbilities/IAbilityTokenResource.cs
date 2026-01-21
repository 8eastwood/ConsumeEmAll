using System;

public interface IAbilityTokenResource
{
    int CurrentAmount { get; }
    event Action<int> AmountChanged;
    bool TryConsume();
    void AddTokens(int amount);
}
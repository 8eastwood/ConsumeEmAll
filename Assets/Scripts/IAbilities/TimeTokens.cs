using System;
using UnityEngine;

public class TimeTokens : MonoBehaviour
{
    [SerializeField] private int _amountOfTokens;
    
    private int _tokens;
    
    public int Tokens => _tokens;

    public event Action<int> TokensChanged;

    private void Start()
    {
        SetTokens();
    }

    public void RemoveToken()
    {
        if (_tokens > 0)
        {
            _tokens -= 1;
            TokensChanged?.Invoke(_tokens);
        }
    }

    private void SetTokens() 
    {
        _tokens = _amountOfTokens;
        TokensChanged?.Invoke(_tokens);
    }

    private void AddTokens()  // реализовать входящее количество через магазин
    {
        _tokens += _amountOfTokens;
        TokensChanged?.Invoke(_tokens);
    }
}
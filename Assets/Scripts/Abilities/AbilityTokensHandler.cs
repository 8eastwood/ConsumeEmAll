using UnityEngine;

public class AbilityTokensHandler : MonoBehaviour
{
    [SerializeField] private int _amountOfTokens;
    private int _tokens;
    public int Tokens => _tokens;

    private void Start()
    {
        AddTokens();
    }

    public void RemoveToken()
    {
        _tokens -= 1;
    }

    private void AddTokens() // реализовать входящее количество через магазин
    {
        _tokens += _amountOfTokens;
    }
}
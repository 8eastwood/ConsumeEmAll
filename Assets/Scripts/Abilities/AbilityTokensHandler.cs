using UnityEngine;

public class AbilityTokensHandler : MonoBehaviour
{
    [SerializeField] private AddTimeAbility _timeAbility;

    private int _tokens;
    private int _amount = 1;
    public int Tokens => _tokens;

    private void Start()
    {
        AddTokens();
        _amount -= 1;
    }
    
    public void RemoveToken()
    {
        _tokens -= 1;
    }

    private void AddTokens() // реализовать входящее количество через магазин
    {
        _tokens += _amount;
    }
}
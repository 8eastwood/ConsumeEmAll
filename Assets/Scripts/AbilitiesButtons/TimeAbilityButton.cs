using System;
using UnityEngine;


public class TimeAbilityButton : ButtonListener
{
    [SerializeField] private AbilityTokensHandler _tokensHandler;
    [SerializeField] private Timer _timer;
    [Space] 
    [SerializeField] private float _timeAmount;
    
    public event Action ButtonClicked;
    
    protected override void ClickOnButton()
    {
        Debug.Log($"Button clicked");
        OnButtonClick();
    }
    
    private void OnButtonClick()
    {
        if (_tokensHandler.Tokens > 0)
        {
            _timer.AddMoreTime(_timeAmount);
            _tokensHandler.RemoveToken();
        }
        else
            Debug.Log("can't use ability rn");
    }
}

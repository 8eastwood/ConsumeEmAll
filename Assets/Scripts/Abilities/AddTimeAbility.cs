using UnityEngine;

public class AddTimeAbility : MonoBehaviour
{
    [SerializeField] private AbilityTokensHandler _tokensHandler;
    [SerializeField] private TimeAbilityButton _timeAbilityButton;
    [SerializeField] private Timer _timer;
    [Space] 
    [SerializeField] private float _timeAmount;

    private void OnEnable()
    {
        _timeAbilityButton.ButtonClicked += OnButtonClick;
    }

    private void OnDisable()
    {
        _timeAbilityButton.ButtonClicked -= OnButtonClick;
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
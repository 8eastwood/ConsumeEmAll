using System;
using UnityEngine;
using UnityEngine.UI;


public class TimeAbilityButton : ButtonListener
{
    [Header("Components")]
    [SerializeField] private IAbilityTokenResource _tokenResource;
    [SerializeField] private Timer _timer;
    [Space] 
    [SerializeField] private float _timeAmount;

    [Header("UI Elements")]
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;
    

    private void Awake()
    {
        UpdateButtonVisual();
    }

    private void OnEnable()
    {
        _tokenResource.AmountChanged += OnTimeTokensChanged;
    }

    private void OnDisable()
    {
        _tokenResource.AmountChanged -= OnTimeTokensChanged;
    }
    
    protected override void OnClickButton()
    {
        Debug.Log($"Button clicked");
        OnButtonClick();
    }
    
    private void OnButtonClick()
    {
        if (_tokenResource.Tokens > 0)
        {
            _timer.AddMoreTime(_timeAmount);
            _tokenResource.RemoveToken();
        }
        else
            Debug.Log("can't use ability rn");
    }

    private void OnTimeTokensChanged(int tokens)
    {
        UpdateButtonVisual();
    }

    private void UpdateButtonVisual()
    {
        if (_buttonImage == null)
            return;

        if (_tokenResource.Tokens > 0)
        {
            _buttonImage.sprite = _enabledSprite;
        }
        else
        {
            _buttonImage.sprite = _disabledSprite;
        }
    }
}

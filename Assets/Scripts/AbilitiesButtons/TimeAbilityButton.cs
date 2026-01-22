using UnityEngine;
using UnityEngine.UI;

public class TimeAbilityButton : ButtonListener
{
    [Header("Components")]
    [SerializeField] private Timer _timer;
    [SerializeField] private TimeTokenResource _tokenResource;

    [Header("UI Elements")]
    [SerializeField] private Image _buttonImage;

    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    [Space]
    [SerializeField] private float _timeAmount;

    private void Awake()
    {
        // if (_tokenResource == null)
        // {
        //     _tokenResource = GetComponent<IAbilityTokenResource>();
        // }

        UpdateButtonVisual();
    }

    private void OnEnable()
    {
        _tokenResource.AmountChanged += OnTokensChanged;
    }

    private void OnDisable()
    {
        _tokenResource.AmountChanged -= OnTokensChanged;
    }

    protected override void OnClickButton()
    {
        Debug.Log("Button clicked");

        if (_tokenResource.TryConsume())
        {
            _timer.AddMoreTime(_timeAmount);
            // _tokenResource.RemoveToken();
        }
        else
            Debug.Log("can't use ability rn");
    }

    private void OnTokensChanged(int tokens)
    {
        UpdateButtonVisual();
    }

    private void UpdateButtonVisual()
    {
        if (_buttonImage == null)
            return;

        if (_tokenResource.CurrentAmount > 0)
            _buttonImage.sprite = _enabledSprite;
        else
            _buttonImage.sprite = _disabledSprite;
    }
}
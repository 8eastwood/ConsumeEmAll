using UnityEngine;
using UnityEngine.UI;

public class CutterAbilityButton : MonoBehaviour
{
    [SerializeField] private CutterTokenResource _tokenResource;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;

    private void Awake()
    {
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

    private void OnTokensChanged(int tokens)
    {
        UpdateButtonVisual();
    }

    private void UpdateButtonVisual()
    {
        if (_buttonImage == null || _tokenResource == null)
            return;

        if (_tokenResource.CurrentAmount > 0)
        {
            _buttonImage.sprite = _enabledSprite;
        }
        else
        {
            _buttonImage.sprite = _disabledSprite;
        }
    }
}
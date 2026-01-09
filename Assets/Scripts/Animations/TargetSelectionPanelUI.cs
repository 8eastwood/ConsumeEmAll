using UnityEngine;
using DG.Tweening;

public class TargetSelectionPanelUI : MonoBehaviour
{
    [SerializeField] protected GameObject[] _iconsToHide;
    [SerializeField] private RectTransform _startPosition;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private CanvasGroup _alphaGroup;

    private Vector2 _targetBodyPosition;
    private Sequence _animation;

    private void Awake()
    {
        _targetBodyPosition = _panel.anchoredPosition;
        _alphaGroup.alpha = 0;
    }

    public void ShowSelectionPanel()
    {
        _animation = DOTween.Sequence();

        if (_iconsToHide != null && _iconsToHide.Length > 0)
            HideIcons();

        _animation
            .Append(_alphaGroup.DOFade(1, .5f).From(0))
            .Join(_panel.DOAnchorPos(_targetBodyPosition, .5f).From(_startPosition.anchoredPosition));
    }

    public void HideSelectionPanel()
    {
        _animation = DOTween.Sequence();

        _animation
            .Append(_alphaGroup.DOFade(0, .5f).From(1))
            .Join(_panel.DOAnchorPos(_startPosition.anchoredPosition, .5f).From(_targetBodyPosition))
            .OnComplete(() => transform.gameObject.SetActive(false));

        ShowIcons();
    }

    private void HideIcons()
    {
        foreach (var icon in _iconsToHide)
        {
            icon.transform.DOScale(0, .3f).SetEase(Ease.InBack);
        }
    }

    private void ShowIcons()
    {
        foreach (var icon in _iconsToHide)
        {
            icon.transform.DOScale(1, .3f).SetEase(Ease.InBack);
        }
    }
}
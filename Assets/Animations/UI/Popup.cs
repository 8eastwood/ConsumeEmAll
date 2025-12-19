using UnityEngine;
using DG.Tweening;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public abstract class Popup : MonoBehaviour
{
    [SerializeField] protected RectTransform _window;
    [SerializeField] protected CanvasGroup _alphaGroup;
    [SerializeField] protected Button _button;
    [SerializeField] protected RectTransform _startPosition;
    [SerializeField] protected Image _background;
    [SerializeField] protected GameObject[] _iconsToHide;

    protected Vector2 _targetBodyPosition;
    protected Sequence _animation;

    private void Awake()
    {
        _targetBodyPosition = _window.anchoredPosition;
        _alphaGroup.alpha = 0;
    }

    public virtual void Show()
    {
        KillCurrentAnimationIfActive();

        if (_iconsToHide != null && _iconsToHide.Length > 0)
            HideIcons();

        _animation = DOTween.Sequence();

        _animation
            .Append(_alphaGroup.DOFade(1, 1f).From(0))
            .Join(_background.DOFade(.5f, .5f).From(0))
            .Join(_window.DOAnchorPos(_targetBodyPosition, .7f).From(_startPosition.anchoredPosition))
            .Append(_button.transform.DOScale(1, .5f).From(0).SetEase(Ease.OutBounce));
    }

    public virtual void Hide()
    {
        _animation = DOTween.Sequence();

        _animation
            .Append(_alphaGroup.DOFade(0, .5f).From(1))
            .Join(_window.DOAnchorPos(_startPosition.anchoredPosition, 1f).From(_targetBodyPosition))
            .OnComplete(() => transform.gameObject.SetActive(false));
    }

    public bool InAnimation() => _animation != null && _animation.active;

    private void OnDestroy()
    {
        KillCurrentAnimationIfActive();
    }

    private void KillCurrentAnimationIfActive()
    {
        if (InAnimation())
            _animation.Kill();
    }

    private void HideIcons()
    {
        foreach (var icon in _iconsToHide)
        {
            icon.transform.DOScale(0, .3f).SetEase(Ease.InBack);
        }
    }
}
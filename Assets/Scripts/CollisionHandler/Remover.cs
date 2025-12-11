using UnityEngine;
using DG.Tweening;

// [RequireComponent(typeof(ShapeAnimator))]
public class Remover : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private ColorIdentity _colorIdentity;
    [SerializeField] private ShapeAnimator _shapeAnimator;
    [Header("Settings")]
    [SerializeField] private int _maxUnitsToCollect;

    private int _collectedUnit;

    private void OnEnable()
    {
        _collisionHandler.UnitReached += TryCollectUnit;
    }

    private void OnDisable()
    {
        _collisionHandler.UnitReached -= TryCollectUnit;
    }

    private void TryCollectUnit(Bomb bomb)
    {
        ColorIdentity bombColor = bomb.GetComponent<ColorIdentity>();
        BombAnimator unitAnimator = bomb.GetComponent<BombAnimator>();

        // Debug.Log(bombColor.Color);

        if (bombColor != null && bombColor.Color == _colorIdentity.Color)
        {
            unitAnimator.PlayDefuseAnimation();
            _collectedUnit++;
            
            if (_collectedUnit >= _maxUnitsToCollect)
                // this.transform.DOScale(0, .3f).SetEase(Ease.InBack);
                PlayScaleDownAnimation();
            
            // Debug.Log(_collectedUnit);
            // unit.Remove();
        }
    }

    private void PlayScaleDownAnimation()
    {
        // _shapeAnimator.Append()
        _shapeAnimator.PlayScaleDownAnimation();
    }
}
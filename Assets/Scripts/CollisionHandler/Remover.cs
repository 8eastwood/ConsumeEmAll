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
    [SerializeField] private int _maxBombsToCollect;

    private int _collectedBombs;

    public ColorType Color => _colorIdentity.Color;

    private void OnEnable()
    {
        _collisionHandler.UnitReached += TryCollectBomb;
        BombEvents.OnBombDestroyed += OnBombDestroyedByAbility;
    }

    private void Update()
    {
    }

    private void OnDisable()
    {
        _collisionHandler.UnitReached -= TryCollectBomb;
        BombEvents.OnBombDestroyed -= OnBombDestroyedByAbility;
    }

    private void OnBombDestroyedByAbility(Bomb bomb, ColorType colorType)
    {
        if (colorType == _colorIdentity.Color)
        {
            IncrementCollectedCount();
        }
    }

    private void TryCollectBomb(Bomb bomb)
    {
        ColorIdentity bombColor = bomb.GetComponent<ColorIdentity>();
        BombAnimator unitAnimator = bomb.GetComponent<BombAnimator>();

        if (bombColor != null && bombColor.Color == _colorIdentity.Color)
        {
            unitAnimator.PlayDefuseAnimation();
            IncrementCollectedCount();

            // Debug.Log(_collectedUnit);
            // unit.Remove();
        }
    }

    private void IncrementCollectedCount()
    {
        _collectedBombs++;
        Debug.Log(_colorIdentity.Color + " have " + _collectedBombs);

        if (_collectedBombs >= _maxBombsToCollect)
            PlayScaleDownAnimation();
    }

    private void PlayScaleDownAnimation()
    {
        _shapeAnimator.PlayScaleDownAnimation();
        // gameObject.SetActive(false);
    }
}
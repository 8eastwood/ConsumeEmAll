using UnityEngine;
using DG.Tweening;

// [RequireComponent(typeof(ShapeAnimator))]
public class BombDisarmer : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private ColorIdentity _colorIdentity;
    [SerializeField] private ShapeAnimator _shapeAnimator;
    [SerializeField] private VictoryConditionChecker _victoryConditionChecker;
    [Header("Settings")]
    [SerializeField] private int _maxBombsToCollect;

    private int _collectedBombs;

    public int MaxBombsToCollect => _maxBombsToCollect;

    public ColorType Color => _colorIdentity.Color;

    private void OnEnable()
    {
        BombEvents.OnBombDestroyed += OnBombDestroyedByAbility;
    }

    private void OnDisable()
    {
        BombEvents.OnBombDestroyed -= OnBombDestroyedByAbility;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Bomb bomb))
        {
            if (bomb.IsCollected)
                return;

            TryCollectBomb(bomb);
        }
    }

    private void OnBombDestroyedByAbility(Bomb bomb, ColorType bombColor)
    {
        if (bombColor == Color)
        {
            IncrementCollectedCount();
        }
    }

    private void TryCollectBomb(Bomb bomb)
    {
        ColorIdentity bombColor = bomb.GetComponent<ColorIdentity>();

        if (bombColor != null && bombColor.Color == _colorIdentity.Color)
        {
            bomb.ChangeCollectedState();
            bomb.PlayDefuseAnimation();
            IncrementCollectedCount();
            _victoryConditionChecker.ChangeCollectedValue();
        }
    }

    private void IncrementCollectedCount()
    {
        _collectedBombs++;

        if (_collectedBombs >= _maxBombsToCollect)
            PlayScaleDownAnimation();
    }

    private void PlayScaleDownAnimation()
    {
        _shapeAnimator.PlayScaleDownAnimation();
    }
}
using UnityEngine;

public class Remover : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private ColorIdentity _colorIdentity;

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
            Debug.Log(bombColor.Color);
            // unit.Remove();
        }
    }
}
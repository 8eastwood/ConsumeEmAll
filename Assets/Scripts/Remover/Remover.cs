using UnityEngine;

public class Remover : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private ColorIdentity _colorIdentity;

    private void OnEnable()
    {
        _collisionHandler.UnitReached += CollectUnit;
    }

    private void OnDisable()
    {
        _collisionHandler.UnitReached -= CollectUnit;
    }

    private void CollectUnit(Unit unit)
    {
        ColorIdentity unitColor = unit.GetComponent<ColorIdentity>();
        
        Debug.Log(unitColor.Color);

        if (unitColor != null && unitColor.Color == _colorIdentity.Color)
        {
            Debug.Log(unitColor.Color);
            unit.Remove();
        }
    }
}
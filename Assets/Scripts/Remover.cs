using UnityEngine;

public class Remover : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;

    private void OnEnable()
    {
        _collisionHandler.UnitReached += Collect;
    }

    private void OnDisable()
    {
        _collisionHandler.UnitReached -= Collect;
    }

    private void Collect(Unit unit)
    {
        unit.Remove();
    }
}
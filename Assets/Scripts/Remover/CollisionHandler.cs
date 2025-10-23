using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Unit> UnitReached;
    
    private void OnColliderEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Unit unit))
        {
            UnitReached?.Invoke(unit);
        }
    }
}

using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Bomb> UnitReached;
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Bomb unit))
        {
            UnitReached?.Invoke(unit);
        }
    }
}

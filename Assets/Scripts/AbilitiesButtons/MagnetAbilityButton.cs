// using System.Collections.Generic;
// using UnityEngine;
//
// public class MagnetAbilityButton : ButtonListener
// {
//     [SerializeField] private LayerMask _bombLayerMask;
//
//     private float _radius;
//     private bool _isSelectingTarget = false;
//
//     private void Awake()
//     {
//     }
//
//     private void Update()
//     {
//         if (!_isSelectingTarget)
//             return;
//
//         if (Input.GetMouseButtonDown(0))
//         {
//             StartCoroutine(TrySelectTarget());
//         }
//     }
//
//     private IEnumerator TrySelectTarget()
//     {
//         
//     }
//
//     private void FindBombsInRadius()
//     {
//         Collider[] colliders = new Collider[10];
//
//         int count = Physics.OverlapSphereNonAlloc(
//             transform.position,
//             _radius,
//             colliders,
//             _bombLayerMask
//         );
//
//         List<Bomb> bombs = new();
//
//         for (int i = 0; i < colliders.Length; i++)
//         {
//             Bomb collidedBomb = colliders[i].GetComponent<Bomb>();
//
//             if (collidedBomb != null && collidedBomb.Color == тут цвет фигуры)
//                 bombs.Add(colliders[i].GetComponent<Bomb>());
//         }
//     }
//
//
//     protected override void ClickOnButton()
//     {
//         // Debug.Log("any shape suck some bombs around until it get destroyed");
//     }
// }
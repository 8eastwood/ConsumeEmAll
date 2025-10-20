// using UnityEngine;
//
//
// public class Shape : MonoBehaviour
// {
//    private Rigidbody _rigidbody;
//    private bool IsDragging = false;
//
//    private void Awake()
//    {
//       _rigidbody = GetComponent<Rigidbody>();
//    }
//    
//    private void 
// }

    // надо попробовать внедрить в gridHandler, т.к. я не проверяю на положения мыши в рамках поля
//
// using UnityEngine;
//     
// public class SimpleGridTracker : MonoBehaviour
// {
//     [SerializeField] private GridHandler _gridHandler;
//     
//     private void Update()
//     {
//         if (_gridHandler.TryGetGridPosition(out Vector2 gridPos))
//         {
//             Vector2Int cellPos = new Vector2Int((int)gridPos.x, (int)gridPos.y);
//             
//             // Делайте что-то с позицией
//             Debug.Log($"Mouse in cell: {cellPos}");
//             
//             // Получаем мировые координаты
//             Vector3 worldPos = _gridHandler.GetWorldPosition(cellPos);
//             
//             // Используйте worldPos для визуализации и т.д.
//         }
//     }
// }

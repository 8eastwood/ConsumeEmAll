using UnityEngine;

public class newGridHandler : MonoBehaviour {
	[SerializeField] private Transform _gridPivot;
	[SerializeField] private Vector2Int _gridSize;
	[SerializeField] private Vector2 _cellSize;

	[SerializeField] private DesktopInput _desktopInput;

	public bool TryGetValidGridCoord(Vector3 hitPoint, out Vector2 position) {
		position = Vector2.zero;
		hitPoint -= _gridPivot.position;
		position.x = Mathf.FloorToInt(hitPoint.x / _cellSize.x);
		position.y = Mathf.FloorToInt(hitPoint.z / _cellSize.y);

		return IsGridCoordInBorders(position);
	}

	private bool IsGridCoordInBorders(Vector2 position) {
		return position.x >= 0 &&
		       position.x < _gridSize.x &&
		       position.y >= 0 &&
		       position.y < _gridSize.y;
	}
}
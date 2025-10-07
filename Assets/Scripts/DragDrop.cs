using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private GameObject _selectedObject;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                        return;
                    
                    _selectedObject = hit.collider.gameObject; 
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                _selectedObject.transform.position = new Vector3(
                    worldPosition.x,
                    0f,
                    worldPosition.z);

                _selectedObject = null;
                Cursor.visible = true;
            }
        }
        
        if(_selectedObject != null)
        {
            Vector3 position = new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            _selectedObject.transform.position = new Vector3(
                worldPosition.x,
                .25f,
                worldPosition.z);
        }
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePositionFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePositionNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        
        Vector3 worldMousePositionFar = Camera.main.ScreenToWorldPoint(screenMousePositionFar);
        Vector3 worldMousePositionNear = Camera.main.ScreenToWorldPoint(screenMousePositionNear);
        
        RaycastHit hit;
        Physics.Raycast(worldMousePositionNear,
            worldMousePositionFar - worldMousePositionNear,
            out hit);
        
        return hit;
    }
}

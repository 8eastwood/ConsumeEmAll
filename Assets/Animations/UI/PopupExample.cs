using UnityEngine;

public class PopupExample : MonoBehaviour
{
    [SerializeField] private Popup _popup;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _popup.gameObject.SetActive(true);
            _popup.Show();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _popup.Hide();
        }
    }
}

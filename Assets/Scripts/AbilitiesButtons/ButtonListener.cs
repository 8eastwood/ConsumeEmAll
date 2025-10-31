using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonListener : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(ClickOnButton);
    }

    protected abstract void ClickOnButton();
}

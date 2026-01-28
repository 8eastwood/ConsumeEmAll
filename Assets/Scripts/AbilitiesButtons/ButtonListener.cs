using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonListener : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    protected abstract void OnClickButton();
}

using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonListener : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void Start()
    {
        _button.onClick.AddListener(OnClickButton);
        //kjhgfgfhjk
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }

    protected abstract void OnClickButton();
}

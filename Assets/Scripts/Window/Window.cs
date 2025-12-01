using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _button;

    protected CanvasGroup WindowGroup => _windowGroup;
    protected Button Button => _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public abstract void Open();
    public abstract void Close();
    protected abstract void OnButtonClick();
}
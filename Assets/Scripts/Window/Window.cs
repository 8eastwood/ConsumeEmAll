using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup _windowGroup;
    [SerializeField] private Button _restartButton;

    protected CanvasGroup WindowGroup => _windowGroup;
    protected Button RestartButton => _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnButtonClick);
    }

    public abstract void Open();
    public abstract void Close();
    protected abstract void OnButtonClick();
}
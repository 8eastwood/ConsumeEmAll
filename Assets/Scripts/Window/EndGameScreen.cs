using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    private void Start()
    {
        Close();
    }

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        RestartButton.interactable = false;
        gameObject.SetActive(false);
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        WindowGroup.alpha = 1f;
        RestartButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
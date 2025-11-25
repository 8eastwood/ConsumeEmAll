using System;
using UnityEngine;

public class GameOverScreen : Window
{
    [SerializeField] private GameOverAnimation _animation;

    public event Action RestartButtonClicked;

    public override void Close()
    {
        _animation.Hide();
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        _animation.Show();
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
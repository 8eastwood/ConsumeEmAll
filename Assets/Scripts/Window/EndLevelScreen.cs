using System;
using UnityEngine;

public class EndLevelScreen : Window
{
    [SerializeField] private EndLevelAnimation _endLevelAnimation;
    
    public event Action ContinueButtonClicked;
    
    // private void 
    public override void Open()
    {
        gameObject.SetActive(true);
        _endLevelAnimation.Show();
    }

    public override void Close()
    {
        _endLevelAnimation.Hide();
    }

    protected override void OnButtonClick()
    {
        ContinueButtonClicked?.Invoke();
    }
}
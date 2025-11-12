using System;

public class TimeAbilityButton : ButtonListener
{
    public event Action ButtonClicked;
    
    protected override void ClickOnButton()
    {
        ButtonClicked?.Invoke();
    }
}

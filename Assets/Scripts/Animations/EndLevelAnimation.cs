using System;
using DG.Tweening;

public class EndLevelAnimation : Popup
{
    public event Action EndLevelAnimationComplete;

    public override void Show()
    {
        base.Show();

        _animation.OnComplete(() => EndLevelAnimationComplete?.Invoke());
    }
}

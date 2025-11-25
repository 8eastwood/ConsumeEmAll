using System;
using DG.Tweening;

public class GameOverAnimation : Popup
{
    public event Action OnAnimationComplete;

    public override void Show()
    {
        base.Show();

        _animation.OnComplete(() => OnAnimationComplete?.Invoke());
    }
}
using System;
using DG.Tweening;

public class GameOverAnimation : Popup
{
    public event Action GameOverAnimationComplete;

    public override void Show()
    {
        base.Show();

        _animation.OnComplete(() => GameOverAnimationComplete?.Invoke());
    }
}
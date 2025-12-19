using System;

public class BombEvents
{
    public static event Action<Bomb, ColorType> OnBombDestroyed;

    public static void NotifyBombDestroyed(Bomb bomb, ColorType color)
    {
        OnBombDestroyed?.Invoke(bomb, color);
    }
}


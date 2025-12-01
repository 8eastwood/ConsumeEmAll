using System;
using UnityEngine;

public class BombPresenceScanner : MonoBehaviour
{
    [SerializeField] private float _checkInterval = 0.3f;

    public event Action NoBombLeft;

    private void Start()
    {
        InvokeRepeating(nameof(CheckPresence), 0f, _checkInterval);
    }

    private void CheckPresence()
    {
        int bombCount = FindObjectsOfType<Bomb>().Length;

        if (bombCount <= 0)
        {
            CancelInvoke(nameof(CheckPresence));
            NoBombLeft?.Invoke();
        }
    }
}
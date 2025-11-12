using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private DesktopInput _desktopInputReader;
    [Space]
    [SerializeField] private float _time;

    private float _timeLeft = 0f;
    private bool _timerStarted = false;

    public event Action GameOver;

    private void Start()
    {
        _timeLeft = _time;
        _timerStarted = false;
    }

    private void Update()
    {
        if (_desktopInputReader.IsPointerDown && !_timerStarted)
        {
            _timerStarted = true;
            StartCoroutine(StartTimer());
        }
    }
    
    public void AddMoreTime(float time)
    {
        _timeLeft += time;
    }

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();

            if (_timeLeft <= 0)
                GameOver?.Invoke();

            yield return null;
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
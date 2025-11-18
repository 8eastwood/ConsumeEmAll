using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private BombPresenceScanner _bombScanner;
    [SerializeField] private Score _score;
    [SerializeField] private TimeRewardSystem _timeRewardSystem;
    [Header("Settings")]
    [SerializeField] private Timer _timer;
    

    private void OnEnable()
    {
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _timer.GameOver += OnGameOver;
        _bombScanner.NoBombLeft += OnLevelComplete;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
        _bombScanner.NoBombLeft -= OnLevelComplete;
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _endGameScreen.Open();
    }

    private void OnLevelComplete()
    {
        _score.AddScore(_timeRewardSystem.CalculateReward());
        
    }
}
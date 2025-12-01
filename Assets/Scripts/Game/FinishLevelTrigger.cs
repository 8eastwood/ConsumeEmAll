using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelTrigger : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Score _score;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private EndLevelScreen _endLevelScreen;
    [SerializeField] private BombPresenceScanner _bombScanner;
    [SerializeField] private TimeRewardSystem _timeRewardSystem;
    [SerializeField] private GameOverAnimation _gameOverAnimation;
    [SerializeField] private EndLevelAnimation _endLevelAnimation;
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [Header("Settings")] 
    [SerializeField] private Timer _timer;


    private void OnEnable()
    {
        _gameOverScreen.RestartButtonClicked += OnRestartButtonClick;
        _timer.GameOver += OnGameOver;
        _bombScanner.NoBombLeft += OnLevelComplete;
        _gameOverAnimation.GameOverAnimationComplete += StopGame;
        _endLevelScreen.ContinueButtonClicked +=  NextScene;
        _endLevelAnimation.EndLevelAnimationComplete += StopGame;
    }

    private void Start()
        
    {
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        _timer.GameOver -= OnGameOver;
        _gameOverScreen.RestartButtonClicked -= OnRestartButtonClick;
        _bombScanner.NoBombLeft -= OnLevelComplete;
        _gameOverAnimation.GameOverAnimationComplete -= StopGame;
        _endLevelScreen.ContinueButtonClicked -=  NextScene;
        _endLevelAnimation.EndLevelAnimationComplete -= StopGame;
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGameOver()
    {
        _gameOverScreen.Open();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
    }

    private void OnLevelComplete()
    {
        _score.AddScore(_timeRewardSystem.CalculateReward());
        _score.SetScore();
        _endLevelScreen.Open();
    }

    private void NextScene()
    {
        _sceneSwitcher.ChangeScene();
    }
}
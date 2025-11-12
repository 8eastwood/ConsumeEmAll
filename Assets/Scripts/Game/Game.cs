using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private Timer _timer;

    private void OnEnable()
    {
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
        _timer.GameOver +=
    }

    private void OnDisable()
    {
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
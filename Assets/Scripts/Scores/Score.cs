using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _score;
    private string _scoreKey = "Score";

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            CurrentScore = 0;
        else
            CurrentScore = PlayerPrefs.GetInt(_scoreKey, 0);
    }

    private void Start()
    {
        _scoreText.text = $"Score: {CurrentScore.ToString()}";
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = $"Score: {_score.ToString()}";
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt(_scoreKey, _score);
    }
}
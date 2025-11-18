using UnityEngine;

public class Score : MonoBehaviour
{
    private int _score;
    
    public void AddScore(int score)
    {
        _score += score;
        Debug.Log(_score);
    }
}
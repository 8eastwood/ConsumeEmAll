using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private BombPresenceScanner _scanner;

    private int _nextSceneIndex = 1;

    private void OnEnable()
    {
        _scanner.NoBombLeft += ChangeScene;
    }

    private void OnDisable()
    {
        _scanner.NoBombLeft -= ChangeScene;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _nextSceneIndex);
    }
}
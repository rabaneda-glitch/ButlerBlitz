using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;

    bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("MovementButler");
    }

    public void ExitToMenu()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}

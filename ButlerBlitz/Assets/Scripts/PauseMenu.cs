using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject enterMenu;
    public GameObject exitMenu;
    public KeyCode pauseKey = KeyCode.Escape;

    void Start()
    {
        pauseMenu.SetActive(false);
        enterMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(pauseKey))
            Pause();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Menu(string StartMenu)
    {
        SceneManager.LoadScene(StartMenu);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}

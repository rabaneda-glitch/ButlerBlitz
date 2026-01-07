using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject enterMenu;
    public GameObject controlsMenu;
    public GameObject optionsMenu;
    public KeyCode pauseKey = KeyCode.Escape;

    public static bool CameFromGame = false;


    void Start()
    {
        pauseMenu.SetActive(false);
        enterMenu.SetActive(false);
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
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
        CameFromGame = true;
        Time.timeScale = 1f;
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

}

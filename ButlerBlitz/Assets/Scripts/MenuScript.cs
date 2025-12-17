using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject currentState;
    private Coroutine splashTimerCoroutine;

    public enum MenuStates
    {
        Splash,
        Main,
        Levels,
        Options,
        Credits,
    };

    public GameObject splashMenu;
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    void Start()
    {
        splashMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        if (GameManager.CameFromGameOver || PauseMenu.CameFromGame)
        {
            currentState = mainMenu;
            switchMenu(MenuStates.Main);
        }
        else
        {
            currentState = splashMenu;
            switchMenu(MenuStates.Splash);
        }
    }

    public void back()
    {
        Debug.Log("back to main menu");
        switchMenu(MenuStates.Main);
    }

    public void menu()
    {
        Debug.Log("go to menu");
        switchMenu(MenuStates.Main);
    }

    public void levels()
    {
        Debug.Log("levels selected");
        switchMenu(MenuStates.Levels);
    }

    public void options()
    {
        Debug.Log("options selected");
        switchMenu(MenuStates.Options);
    }

    public void credits()
    {
        Debug.Log("credits selected");
        switchMenu(MenuStates.Credits);
    }

    public void levelOne()
    {
        Debug.Log("level 1 selected");
        SceneManager.LoadScene("MovementButler");
    }

    public void quit()
    {
        Debug.Log("quitting game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void switchMenu(MenuStates menu)
    {
        GameObject newState;

        switch (menu)
        {
            case MenuStates.Splash:
                newState = splashMenu;
                break;
            case MenuStates.Main:
                newState = mainMenu;
                break;
            case MenuStates.Levels:
                newState = levelMenu;
                break;
            case MenuStates.Options:
                newState = optionsMenu;
                break;
            case MenuStates.Credits:
                newState = creditsMenu;
                break;
            default:
                newState = mainMenu;
                break;
        }

        // Desactivar el menú anterior antes de cambiar
        if (currentState != null)
            currentState.SetActive(false);

        if (splashTimerCoroutine != null)
        {
            StopCoroutine(splashTimerCoroutine);
            splashTimerCoroutine = null;
        }

        currentState = newState;
        currentState.SetActive(true);

        if (currentState == splashMenu && !GameManager.CameFromGameOver && !PauseMenu.CameFromGame)
        {
            splashTimerCoroutine = StartCoroutine(SplashTimer());
        }
    }

    private System.Collections.IEnumerator SplashTimer()
    {
        yield return new WaitForSeconds(10f);

        if (splashMenu != null && splashMenu.activeSelf)
        {
            menu();
        }

        splashTimerCoroutine = null;
    }

    void Update()
    {
        if (splashMenu.activeSelf && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            menu();
        }

        if (Input.GetKey("return") && mainMenu.activeSelf)
        {
            levels();
        }

        if (Input.GetKey("escape"))
        {
            if (mainMenu.activeSelf || splashMenu.activeSelf)
            {
                Application.Quit();
                Debug.Log("Saliendo del juego");
            }
            else
            {
                menu();
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GameObject currentState;

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
        currentState = splashMenu;
        splashMenu.SetActive(true);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
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

        currentState = newState;
        currentState.SetActive(true);
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

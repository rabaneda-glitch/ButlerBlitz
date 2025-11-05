using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private GameObject currentState;

    public enum MenuStates
    {
        Main,
        Levels,
    };

    public GameObject mainMenu;
    public GameObject levelMenu;

    void Start()
    {
        currentState = mainMenu;
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void levels()
    {
        Debug.Log("levels selected");
        switchMenu(MenuStates.Levels);
    }

    public void back()
    {
        Debug.Log("back to main menu");
        switchMenu(MenuStates.Main);
    }

    public void switchMenu(MenuStates menu)
    {
        GameObject newState;

        switch (menu)
        {
            case MenuStates.Main:
                newState = mainMenu;
                break;
            case MenuStates.Levels:
                newState = levelMenu;
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
}

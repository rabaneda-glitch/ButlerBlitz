using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCinematicScript : MonoBehaviour
{
   private GameObject currentState;

    public int menuState = 0;

    public GameObject firstImage;
    public GameObject secondImage;
    public GameObject thirdImage;
    public GameObject fourthImage;

    void Start()
    {
        currentState = firstImage;
        firstImage.SetActive(true);
        secondImage.SetActive(false);
        thirdImage.SetActive(false);
        fourthImage.SetActive(false);
    }



    public void next()
    {
        Debug.Log("next image selected");
        menuState += 1;
        if (menuState > 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            switchImage(menuState);
        }
    }



    public void switchImage(int menu)
    {
        GameObject newState;

        switch (menuState)
        {
            case 0:
                newState = firstImage;
                break;
            case 1:
                newState = secondImage;
                break;
            case 2:
                newState = thirdImage;
                break;
            case 3:
                newState = fourthImage;
                break;
            default:
                newState = firstImage;
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

        if (Input.GetKey("return"))
        {
            next();

        }
        if (Input.GetKey("escape"))
        {
            next();

        }
    }
}

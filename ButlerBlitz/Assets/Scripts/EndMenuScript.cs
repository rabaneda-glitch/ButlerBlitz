using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuScript : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    string scoreString = "";

    void Start()
    {
     
    }

    public void Continue()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void Update()
    {

        scoreString = ScoreScript.Instance.GetScoreString();

        textScore.text = scoreString;

        // Acciones de entrada
        if (Input.GetKey("return") || Input.GetKey("escape"))
        {
            SceneManager.LoadScene("StartMenu");
        }
    }

    
}

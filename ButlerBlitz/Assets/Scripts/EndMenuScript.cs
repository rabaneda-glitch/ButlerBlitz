using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenuScript : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    string scoreString = "";
    public static bool CameFromGameWon = false;

    void Start()
    {
     
    }

    public void Continue()
    {
        SceneManager.LoadScene("StartMenu");
        CameFromGameWon = true;
    }

    void Update()
    {

        scoreString = ScoreScript.Instance.GetScoreString();

        textScore.text = scoreString;

        // Acciones de entrada
        if (Input.GetKey("return") || Input.GetKey("escape"))
        {
            Continue();
        }
    }

    
}

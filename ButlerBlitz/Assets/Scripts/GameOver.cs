using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Timer timer;
    private MomentumScript momentumScript;
    private GameOverCam gameOverCam;
    private PauseMenu pauseMenuScript;

    public GameObject Player;
    public GameObject Tools;
    public GameObject NPCCocina;
    public GameObject NPCHabitación;
    public GameObject NPCBiblioteca;
    public GameObject CanvasInterface;
    public GameObject CanvasButtons;

    public Camera camMain;
    public Camera camSec;

    public bool pause;
    private bool isGameOverTriggered = false;

    void Start()
    {
        timer = UnityEngine.Object.FindFirstObjectByType<Timer>();
        momentumScript = UnityEngine.Object.FindFirstObjectByType<MomentumScript>();
        gameOverCam = UnityEngine.Object.FindFirstObjectByType<GameOverCam>();
        pauseMenuScript = Object.FindFirstObjectByType<PauseMenu>();

        CanvasButtons.SetActive(false);

        camMain.enabled = true;
        camSec.enabled = false;
    }

    void Update()
    {
        pause = (
           pauseMenuScript != null
           && pauseMenuScript.pauseMenu != null
           && pauseMenuScript.pauseMenu.activeSelf
       );


        if (pause == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        if (timer.timer <= 0 || momentumScript.qtyMmt <= 0)
        {
            camMain.enabled = false;
            camSec.enabled = true;

            Player.SetActive(false);
            Tools.SetActive(false);
            NPCCocina.SetActive(false);
            NPCHabitación.SetActive(false);
            NPCBiblioteca.SetActive(false);
            CanvasButtons.SetActive(true);

            var canvasGroup = CanvasInterface.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }

            if (gameOverCam != null && gameOverCam.HasVisitedAll)
            {
                if (!isGameOverTriggered)
                    StartCoroutine(LoadGameOverAfterDelay());
            }
        }
    }

    public void SkipToGameOver(string GameOver)
    {
        SceneManager.LoadScene(GameOver);
    }

    private IEnumerator LoadGameOverAfterDelay()
    {
        isGameOverTriggered = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }
}

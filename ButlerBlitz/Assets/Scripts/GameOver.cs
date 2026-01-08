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
    public GameObject Canvas;


    public Camera camMain;
    public Camera camSec;

    public bool pause;

    void Start()
    {
        timer = UnityEngine.Object.FindFirstObjectByType<Timer>();
        momentumScript = UnityEngine.Object.FindFirstObjectByType<MomentumScript>();
        gameOverCam = UnityEngine.Object.FindFirstObjectByType<GameOverCam>();
        pauseMenuScript = Object.FindFirstObjectByType<PauseMenu>();

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

        if (timer.timer <= 0 || momentumScript.qtyMmt <= 0)
        {
            Cursor.visible = false;
            camMain.enabled = false;
            camSec.enabled = true;

            Player.SetActive(false);
            Tools.SetActive(false);
            NPCCocina.SetActive(false);
            NPCHabitación.SetActive(false);
            NPCBiblioteca.SetActive(false);
            Canvas.SetActive(false);

            if (gameOverCam != null && gameOverCam.HasVisitedAll)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                SceneManager.LoadScene("GameOver");
            }

            if (pause == true)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}

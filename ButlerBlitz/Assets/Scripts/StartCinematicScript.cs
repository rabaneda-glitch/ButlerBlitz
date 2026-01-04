using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CinematicFrame
{
    public GameObject image;
    public bool hasMovement;
    public bool goesUp,
        goesDown;
    public float targetYPosition = 0f;
    public float speed = 1f;

    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
}

public class StartCinematicScript : MonoBehaviour
{
    public int menuState = 0;

    public CinematicFrame[] frames;

    private CinematicFrame currentFrame;
    public string nextSceneName;
    public Button nextButton;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        currentFrame = frames[menuState];

        foreach (var frame in frames)
            frame.image.SetActive(false);

        currentFrame.image.SetActive(true);
    }

    public void next()
    {
        menuState++;

        if (menuState >= frames.Length)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        switchFrame(menuState);

        EventSystem.current.SetSelectedGameObject(null);
    }

    void switchFrame(int index)
    {
        currentFrame.image.SetActive(false);

        currentFrame = frames[index];
        currentFrame.image.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            next();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if (currentFrame.hasMovement)
        {
            RectTransform imageRT = currentFrame.image.GetComponent<RectTransform>();
            float imageHeight = imageRT.rect.height;
            float positionY = currentFrame.image.transform.position.y;
            Debug.Log("positionY: " + positionY);

            if (currentFrame.goesUp && positionY < currentFrame.targetYPosition)
            {
                //Debug.Log("positionY: " + positionY);
                currentFrame.image.transform.Translate(
                    Vector3.up * currentFrame.speed * Time.deltaTime
                );
            }

            if (currentFrame.goesDown & positionY > currentFrame.targetYPosition)
            {
                //Debug.Log("positionY: " + positionY);
                currentFrame.image.transform.Translate(
                    Vector3.down * currentFrame.speed * Time.deltaTime
                );
            }
        }
    }
}

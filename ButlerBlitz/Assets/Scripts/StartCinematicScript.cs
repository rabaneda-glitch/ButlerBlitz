using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CinematicFrame
{
    public GameObject image;
    public bool hasMovement;
    public bool goesUp, goesDown;
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



    void Start()
    {
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
    }


    void switchFrame(int index)
    {
        currentFrame.image.SetActive(false);

        currentFrame = frames[index];
        currentFrame.image.SetActive(true);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            next();

        if (currentFrame.hasMovement)
        {

            RectTransform imageRT = currentFrame.image.GetComponent<RectTransform>();
            float imageHeight = imageRT.rect.height;
            float positionY = currentFrame.image.transform.position.y;
            Debug.Log("positionY: " + positionY);

            if (currentFrame.goesUp && positionY < currentFrame.targetYPosition)
            {
                //Debug.Log("positionY: " + positionY);
                currentFrame.image.transform.Translate(Vector3.up * currentFrame.speed * Time.deltaTime);
            }

            if (currentFrame.goesDown & positionY > currentFrame.targetYPosition)
            {
                //Debug.Log("positionY: " + positionY);
                currentFrame.image.transform.Translate(Vector3.down * currentFrame.speed * Time.deltaTime);
            }



        }
    }
}

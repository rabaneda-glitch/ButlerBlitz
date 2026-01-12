using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; 

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

    [SerializeField] private GameObject loadingScreen; 
    private bool isLoading = false;                    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        currentFrame = frames[menuState];

        foreach (var frame in frames)
            frame.image.SetActive(false);

        currentFrame.image.SetActive(true);

       
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }

    public void next()
    {
        if (isLoading) return; 

        menuState++;

        if (menuState >= frames.Length)
        {
            LoadNextScene();  
            return;           
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
        if (isLoading) return; 

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            next();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNextScene(); 
            return;
        }

        if (currentFrame.hasMovement)
        {
            RectTransform imageRT = currentFrame.image.GetComponent<RectTransform>();
            float imageHeight = imageRT.rect.height; 
            float positionY = currentFrame.image.transform.position.y;
            Debug.Log("positionY: " + positionY);

            // Usar tiempo no escalado para que la animación siga aunque Time.timeScale == 0
            if (currentFrame.goesUp && positionY < currentFrame.targetYPosition)
            {
                currentFrame.image.transform.Translate(
                    Vector3.up * currentFrame.speed * Time.unscaledDeltaTime
                );
            }

            
            if (currentFrame.goesDown && positionY > currentFrame.targetYPosition) 
            {
                currentFrame.image.transform.Translate(
                    Vector3.down * currentFrame.speed * Time.unscaledDeltaTime
                );
            }
        }
    }

    private void LoadNextScene()
    {
        if (isLoading) return;
        isLoading = true;
        StartCoroutine(LoadAsync(nextSceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

       
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
            yield return null;
    }
}

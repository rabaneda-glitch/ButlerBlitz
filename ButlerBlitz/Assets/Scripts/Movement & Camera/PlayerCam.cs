using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private Camera _camera;

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private PauseMenu pauseMenuScript;
    public bool pause;

    [Header("Crosshair")]
    [SerializeField] private Texture2D crosshair;

    private bool crosshairVisible = true;

    void Start()
    {
        _camera = GetComponent<Camera>();
        pauseMenuScript = Object.FindFirstObjectByType<PauseMenu>();
    }

    void Update()
    {
        pause = (pauseMenuScript != null && pauseMenuScript.pauseMenu != null && pauseMenuScript.pauseMenu.activeSelf);

        if (pause == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            crosshairVisible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crosshairVisible = true;
        }

        //Info del ratón
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotación y orientación de cámara
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void OnGUI()
    {
        if (crosshair == null || !crosshairVisible) return;

        int size = 32;
        float posX = (_camera.pixelWidth - size) / 2;
        float posY = (_camera.pixelHeight - size) / 2;
        GUI.Label(new Rect(posX, posY, size, size), crosshair);
    }
}


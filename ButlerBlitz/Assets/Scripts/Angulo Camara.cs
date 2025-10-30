using UnityEngine;
using UnityEngine.Rendering;

public class AnguloCamara : MonoBehaviour
{
    public enum RotationAxes
    { 
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;
    public float Velocidad = 100f;
    float RotacionX = 0.0f;

    public Transform Player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * Velocidad * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Velocidad * Time.deltaTime;

        if(axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, MouseX, 0);
        }
        else
        {
            RotacionX -= MouseY;
            RotacionX = Mathf.Clamp(RotacionX, -45f, 45f);
            float RotacionY = transform.localEulerAngles.y;
            if(axes == RotationAxes.MouseXandY)
            {
                RotacionY += MouseX;
            }
            transform.localEulerAngles = new Vector3(RotacionX, RotacionY, 0);
        }

    }
}

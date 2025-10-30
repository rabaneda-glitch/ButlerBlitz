using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal"); 
        float deltaZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(deltaX, 10, deltaZ);
        movement = Vector3.ClampMagnitude(movement, 1.0f) * speed;
        movement = transform.TransformDirection(movement); 

        _charController.Move(movement * Time.deltaTime); 
    }
}

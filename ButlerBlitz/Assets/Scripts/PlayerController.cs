using UnityEngine.InputSystem;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;
    private Vector2 moveInput;

    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(Mathf.Abs(jumpHeight * 2 * gravity));
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move: " + moveInput);
    }

    private void Update() 
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");

        Vector3 horizVel = new Vector3(moveInput.x, 0, moveInput.y) * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        movement = Vector3.ClampMagnitude(movement, 1.0f) * speed;
        movement = transform.TransformDirection(movement);

        controller.Move(movement * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; // Para mantenernos tocando el suelo
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            if (velocity.y < -20f) velocity.y = -20f; // Velocidad terminal
        }

        controller.Move((horizVel + velocity) * Time.deltaTime);
    }
}
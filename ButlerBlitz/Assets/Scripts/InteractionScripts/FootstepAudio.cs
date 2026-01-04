using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerMovement playerMovement;
    private Rigidbody rb;

    [Header("Settings")]
    public float minVelocityToPlay = 1.5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (audioSource == null || playerMovement == null || rb == null)
            return;

        // No pasos si está en el aire
        if (!IsGrounded())
        {
            StopSteps();
            return;
        }

        // Velocidad horizontal real
        Vector3 horizontalVelocity = new Vector3(
            rb.linearVelocity.x,
            0f,
            rb.linearVelocity.z
        );

        bool isMoving = horizontalVelocity.magnitude > minVelocityToPlay;

        if (isMoving)
        {
            PlaySteps();
        }
        else
        {
            StopSteps();
        }
    }

    private bool IsGrounded()
    {
        return playerMovement.state != PlayerMovement.MovementState.air;
    }

    private void PlaySteps()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }
    }

    private void StopSteps()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

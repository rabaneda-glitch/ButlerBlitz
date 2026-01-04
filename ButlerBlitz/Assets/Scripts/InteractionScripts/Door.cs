using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator[] anims;
    private bool open = false;
    private const string OPEN_ANIM_PARAM = "Open";

    private Collider doorCollider;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip openSound;
    private AudioSource audioSource;

    void Start()
    {
        anims = GetComponentsInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (anims.Length == 0)
        {
            return;
        }

        foreach (Animator a in anims)
        {
            a.SetBool(OPEN_ANIM_PARAM, false);
        }
        open = false;

        doorCollider = GetComponent<Collider>();
    }

    public void Interact()
    {
        if (ToolManager.Instance != null && ToolManager.Instance.CanOpenDoor())
        {
            OperateDoor();
        }
        else
        {
            KeyTextManager.Instance.ShowTemporaryMessage("Necesitas la llave de la biblioteca", 1.5f);
        }
    }

    private void OperateDoor()
    {
        if (anims == null || anims.Length == 0) return;

        open = !open;

        if (open && openSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        foreach (Animator a in anims)
        {
            a.SetBool(OPEN_ANIM_PARAM, open);
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = !open;
        }
    }
}
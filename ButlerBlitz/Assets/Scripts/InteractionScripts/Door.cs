using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator[] anims;
    private bool open = false;
    private const string OPEN_ANIM_PARAM = "Open";

    private Collider doorCollider;

    void Start()
    {
        anims = GetComponentsInChildren<Animator>();

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
    }

    private void OperateDoor()
    {
        if (anims == null || anims.Length == 0) return;

        open = !open;

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
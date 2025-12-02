using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private bool open = false;

    private const string OPEN_ANIM_PARAM = "Open";
    private Collider doorCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }

        if (anim != null)
        {
            anim.SetBool(OPEN_ANIM_PARAM, false);
            open = false;
        }

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
            Debug.Log("Puerta bloqueada. Necesitas la llave para abrirla.");
        }
    }

    private void OperateDoor()
    {
        if (anim == null) return;

        open = !open;

        if (doorCollider != null)
        {
            doorCollider.enabled = !open;
        }

        anim.SetBool(OPEN_ANIM_PARAM, open);

        Debug.Log(open ? "Puerta Abierta" : "Puerta Cerrada");
    }
}
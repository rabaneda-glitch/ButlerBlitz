// Door.cs (MODIFICADO)

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
            Debug.LogError("No se encontraron Animators en los hijos de " + gameObject.name);
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
            Debug.Log("Puerta bloqueada. Necesitas la llave para abrirla.");
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

        Debug.Log(open ? "Puertas Abiertas" : "Puertas Cerradas");
    }
}
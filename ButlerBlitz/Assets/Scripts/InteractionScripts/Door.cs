// Door.cs

using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private bool open = false; // Estado lógico inicial: la puerta está cerrada

    // Nombre del parámetro booleano que controla la animación de la puerta (debe coincidir con el Animator).
    private const string OPEN_ANIM_PARAM = "Open";
    private Collider doorCollider;

    void Start()
    {
        // Obtiene la referencia al Animator, buscando en el propio objeto o en sus hijos.
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }

        // Inicializa el estado: fuerza el parámetro "Open" en false en el Animator.
        if (anim != null)
        {
            anim.SetBool(OPEN_ANIM_PARAM, false);
            open = false; // Asegura que el estado lógico sea cerrado.
        }

        doorCollider = GetComponent<Collider>();
    }

    /// <summary>
    /// Método llamado por CleanerRay.cs cuando el jugador hace clic en la puerta.
    /// </summary>
    public void Interact()
    {
        // 1. Verificar si el jugador tiene la llave activa en la mano.
        if (ToolManager.Instance != null && ToolManager.Instance.CanOpenDoor())
        {
            // 2. Operar la puerta (cambia el estado y reproduce la animación).
            OperateDoor();
        }
        else
        {
            // Opcional: Proporcionar feedback visual o de sonido (ej. un sonido de "cerradura").
            Debug.Log("Puerta bloqueada. Necesitas la llave para abrirla.");
        }
    }

    /// <summary>
    /// Gestiona la lógica de apertura/cierre y el control del Animator.
    /// </summary>
    private void OperateDoor()
    {
        if (anim == null) return;

        // Invierte el estado lógico de la puerta (cerrada -> abierta, abierta -> cerrada)
        open = !open;

        if (doorCollider != null)
        {
            // Si 'open' es TRUE (se está abriendo), desactiva el collider.
            // Si 'open' es FALSE (se está cerrando), activa el collider.
            doorCollider.enabled = !open;
        }

        // Actualiza el parámetro del Animator, lo que desencadena la transición.
        anim.SetBool(OPEN_ANIM_PARAM, open);

        Debug.Log(open ? "Puerta Abierta" : "Puerta Cerrada");
    }
}
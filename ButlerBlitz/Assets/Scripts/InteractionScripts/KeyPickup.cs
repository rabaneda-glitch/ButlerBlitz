// KeyPickup.cs (Script que va en la Llave del Mundo)
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    // Una referencia al objeto ToolHolder donde está ToolChange.
    // Esto es más seguro que usar FindObjectOfType.
    [SerializeField] private ToolChange toolChangeManager;

    private bool hasBeenPickedUp = false;

    // Asigna el ToolChange Manager en el Inspector.

    public void PickUp()
    {
        if (hasBeenPickedUp) return;

        if (toolChangeManager != null)
        {
            // 1. Llama a la función que creamos en el paso anterior para mostrar la llave en la mano.
            toolChangeManager.SetKeyVisibility(true);

            hasBeenPickedUp = true;

            // 2. Desactiva este objeto de la llave del mundo.
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ToolChangeManager no está asignado en KeyPickup. Asegúrate de arrastrar la referencia en el Inspector.");
        }
    }
}
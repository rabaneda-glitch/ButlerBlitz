// KeyPickup.cs (Script que va en la Llave del Mundo)
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    
    private bool hasBeenPickedUp = false;

    // Asigna el ToolChange Manager en el Inspector.

    public void PickUp()
    {
        if (hasBeenPickedUp) return;

        if (ToolManager.Instance != null && ToolManager.Instance.toolChange != null)
        {
            ToolManager.Instance.toolChange.SetKeyVisibility(true);

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
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
 
    private bool hasBeenPickedUp = false;

    public void PickUp()
    {
        if (hasBeenPickedUp) return;

        if (ToolManager.Instance != null && ToolManager.Instance.toolChange != null)
        {
            ToolManager.Instance.toolChange.SetKeyVisibility(true);

            hasBeenPickedUp = true;

            Destroy(gameObject);
        }
    }
}
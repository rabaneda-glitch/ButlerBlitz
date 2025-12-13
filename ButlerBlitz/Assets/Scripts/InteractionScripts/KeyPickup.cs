using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    private bool hasBeenPickedUp = false;
    public void PickUp()
    {
        if (hasBeenPickedUp) return;

        if (ToolManager.Instance != null && ToolManager.Instance.toolChange != null)
        {
            ToolChange tc = ToolManager.Instance.toolChange;

            ToolManager.Instance.HasKey = true;

            int previousTool = tc.SelectedTool;

           tc.SelectedTool = ToolChange.KEY_TOOL_INDEX;
            
           tc.StartCoroutine(tc.ChangeToolAnimated(previousTool));

            hasBeenPickedUp = true;
            
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ToolManager no está inicializado o ToolChange no está asignado.");
        }
    }
}
using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    private bool hasBeenPickedUp = false;

    [Header("Audio")]
    [SerializeField] private AudioClip pickupSound;

    public void PickUp()
    {
        if (hasBeenPickedUp) return;

        if (ToolManager.Instance != null && ToolManager.Instance.toolChange != null)
        {
            ToolChange tc = ToolManager.Instance.toolChange;

            if (KeyTextManager.Instance != null)
            {
                KeyTextManager.Instance.HideKeyText();
            }

            ToolManager.Instance.HasKey = true;

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            int previousTool = tc.SelectedTool;
            tc.SelectedTool = ToolChange.KEY_TOOL_INDEX;
            tc.StartCoroutine(tc.ChangeToolAnimated(previousTool));

            hasBeenPickedUp = true;

            KeyTextManager.Instance.ShowTemporaryMessage("Llave para la biblioteca obtenida", 3f);

            Destroy(gameObject);
        }
    }
}
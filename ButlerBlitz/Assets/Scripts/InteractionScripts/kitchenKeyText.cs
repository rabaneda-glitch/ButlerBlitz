using UnityEngine;

public class kitchenKeyText : MonoBehaviour
{
    private PlayerMovement player;
    private Zones.CurrentZone lastZone;

    [SerializeField] private float textDuration = 3f;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        lastZone = player != null ? player.currentZone : Zones.CurrentZone.Hall;
    }

    private void Update()
    {
        if (player == null || ToolManager.Instance == null) return;

        var currentZone = player.currentZone;

        if (currentZone != lastZone && currentZone == Zones.CurrentZone.Kitchen)
        {
            if (!ToolManager.Instance.HasKey)
            {
                KeyTextManager.Instance.ShowKeyText(
                    "Coge la llave de la biblioteca"
                );

                CancelInvoke(nameof(HideKeyText));
                Invoke(nameof(HideKeyText), textDuration);
            }
        }

        lastZone = currentZone;
    }

    private void HideKeyText()
    {
        if (KeyTextManager.Instance != null)
            KeyTextManager.Instance.HideKeyText();
    }
}

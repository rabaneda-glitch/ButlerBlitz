using UnityEngine;

public class kitchenKeyText : MonoBehaviour
{
    private PlayerMovement player;

    [Header("Configuración de Distancia")]
    [SerializeField] private float detectionRange = 2.5f;

    private bool isTextShowing = false;
    private bool keyPickedUp = false;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (keyPickedUp || player == null || ToolManager.Instance == null) return;

        if (ToolManager.Instance.HasKey)
        {
            OnKeyCollected();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRange)
        {
            if (!isTextShowing) ShowKeyText();
        }
        else
        {
            if (isTextShowing) HideKeyText();
        }
    }

    private void ShowKeyText()
    {
        if (KeyTextManager.Instance != null)
        {
            KeyTextManager.Instance.ShowKeyText("Coge la llave de la biblioteca");
            isTextShowing = true;
        }
    }

    private void HideKeyText()
    {
        if (KeyTextManager.Instance != null)
        {
            KeyTextManager.Instance.HideKeyText();
            isTextShowing = false;
        }
    }
    private void OnKeyCollected()
    {
        keyPickedUp = true;
        HideKeyText();

        this.enabled = false;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
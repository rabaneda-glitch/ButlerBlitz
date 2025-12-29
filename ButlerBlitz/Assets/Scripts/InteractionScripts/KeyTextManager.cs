using UnityEngine;
using TMPro;
using System.Collections;

public class KeyTextManager : MonoBehaviour
{
    public static KeyTextManager Instance;

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI messageText;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // ---------- TEXTO RECOGER LLAVE ----------
    public void ShowKeyText(string text)
    {
        if (keyText == null) return;

        keyText.text = text;
        keyText.gameObject.SetActive(true);
    }

    public void HideKeyText()
    {
        if (keyText == null) return;

        keyText.gameObject.SetActive(false);
    }

    // ---------- MENSAJE TEMPORAL ----------
    public void ShowTemporaryMessage(string text, float duration)
    {
        if (messageText == null) return;

        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(TemporaryMessageRoutine(text, duration));
    }

    private IEnumerator TemporaryMessageRoutine(string text, float duration)
    {
        messageText.text = text;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        messageText.gameObject.SetActive(false);
        messageCoroutine = null;
    }
}

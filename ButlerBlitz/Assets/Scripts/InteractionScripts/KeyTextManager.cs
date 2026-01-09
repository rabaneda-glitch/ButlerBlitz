using UnityEngine;
using TMPro;
using System.Collections;

public class KeyTextManager : MonoBehaviour
{
    public static KeyTextManager Instance;

    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Background Panels")]
    [SerializeField] private GameObject keyBackground;
    [SerializeField] private GameObject messageBackground;

    private Coroutine messageCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        HideKeyText();
        if (messageText != null) messageText.gameObject.SetActive(false);
        if (messageBackground != null) messageBackground.SetActive(false);
    }

    public void ShowKeyText(string text)
    {
        if (keyText == null) return;

        keyText.text = text;
        keyText.gameObject.SetActive(true);
        if (keyBackground != null) keyBackground.SetActive(true);
    }

    public void HideKeyText()
    {
        if (keyText == null) return;

        keyText.gameObject.SetActive(false);
        if (keyBackground != null) keyBackground.SetActive(false);
    }

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
        if (messageBackground != null) messageBackground.SetActive(true);

        yield return new WaitForSeconds(duration);

        messageText.gameObject.SetActive(false);
        if (messageBackground != null) messageBackground.SetActive(false);

        messageCoroutine = null;
    }
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const float k_MinSlider = 0.0001f;
    private const string k_PlayerPrefKey = "VolumeMusic";

    void Start()
    {
        float saved = PlayerPrefs.GetFloat(k_PlayerPrefKey, 1f);
        SetVolume(saved);

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = saved;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        float clamped = Mathf.Clamp(volume, k_MinSlider, 1f);

        if (audioMixer != null)
            audioMixer.SetFloat("VolumeMusic", Mathf.Log10(clamped) * 20f);

        PlayerPrefs.SetFloat(k_PlayerPrefKey, clamped);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Fullscreen set to: " + isFullscreen);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        Debug.Log("Mouse Sensitivity set to: " + sensitivity);
    }
    
}

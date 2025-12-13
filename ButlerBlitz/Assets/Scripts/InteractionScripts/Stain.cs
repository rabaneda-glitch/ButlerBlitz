using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Stain : MonoBehaviour
{
    public enum StainType { Mud, Dust, Grease, Water }
    public StainType type = StainType.Mud; //por defecto

    [Header("Destruir")]
    [SerializeField] public float destroyDelay = 0.5f;

    private Renderer _renderer;
    private Collider _collider;

    [Header("Sistema de partículas")]
    [SerializeField] private GameObject ParticleSystem;

    [SerializeField] private AudioClip sound;
    private AudioSource cleanSound;

    [Header("Zone")]
    public Zones.CurrentZone assignedZone = Zones.CurrentZone.Hall;
    private Zones _zoneComponent;

    public static Action<Zones.CurrentZone> OnStainCleaned;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();

        var zones = UnityEngine.Object.FindObjectsByType<Zones>(FindObjectsSortMode.None);
        foreach (var z in zones)
        {
            if (z.zone == assignedZone)
            {
                _zoneComponent = z;
                _zoneComponent.stainsInTheZone += 1f;
                break;
            }
        }
    }

    public void Interact(GameObject interactor = null)
    {
        if (ToolManager.Instance != null)
        {
            bool correct = ToolManager.Instance.IsCorrectToolFor(this);
            if (!correct)
            {
                Debug.Log($"Herramienta incorrecta para {type}");
                return;
            }
        }

        StartClean();
    }

    void StartClean()
    {
        if (_collider != null) _collider.enabled = false;
        if (_renderer != null) _renderer.enabled = false;

        var prog = UnityEngine.Object.FindFirstObjectByType<Progresion>();
        if (prog != null)
        {
            prog.IncrementStainsCleaned();
        }

        OnStainCleaned?.Invoke(assignedZone);
        if (_zoneComponent != null)
        {
            _zoneComponent.stainsInTheZone = Mathf.Max(0f, _zoneComponent.stainsInTheZone - 1f);
        }

        // Reproducir sonido
        if (cleanSound != null)
        {
            cleanSound.Play();
        }

        if (ParticleSystem != null)
        {
            GameObject vfxInstance = Instantiate(
                ParticleSystem,
                transform.position,
                Quaternion.identity
            );

            Destroy(vfxInstance, destroyDelay + 1f);
        }

        Destroy(gameObject, destroyDelay);
    }



}

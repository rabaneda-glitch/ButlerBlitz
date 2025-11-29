using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ProgresionPerZones : MonoBehaviour
{
    public Image loadingImage;
    public TextMeshProUGUI loadingText;
    [Range(0, 1)]
    public float loadingProgress = 0;

    public float totalStainsInZone;
    public float stainsCleanedInZone;

    // Mapa con el total inicial de manchas por zona
    private Dictionary<Zones.CurrentZone, int> totalStainsPerZone = new Dictionary<Zones.CurrentZone, int>();
    // Contador de manchas ya limpiadas por zona (se actualiza vía evento)
    private Dictionary<Zones.CurrentZone, int> cleanedPerZone = new Dictionary<Zones.CurrentZone, int>();

    private PlayerMovement player;
    private Zones[] zoneComponents;

    void OnEnable()
    {
        Stain.OnStainCleaned += HandleStainCleaned;
    }

    void OnDisable()
    {
        Stain.OnStainCleaned -= HandleStainCleaned;
    }

    void Start()
    {
        zoneComponents = UnityEngine.Object.FindObjectsByType<Zones>(FindObjectsSortMode.None);

        totalStainsPerZone.Clear();
        cleanedPerZone.Clear();

        var stains = UnityEngine.Object.FindObjectsByType<Stain>(FindObjectsSortMode.None);

        foreach (var s in stains)
        {
            if (totalStainsPerZone.ContainsKey(s.assignedZone))
                totalStainsPerZone[s.assignedZone]++;
            else
                totalStainsPerZone[s.assignedZone] = 1;

            if (!cleanedPerZone.ContainsKey(s.assignedZone))
                cleanedPerZone[s.assignedZone] = 0;
        }

        player = UnityEngine.Object.FindFirstObjectByType<PlayerMovement>();
    }

    // Manejador del evento: aumenta el contador de limpias en la zona indicada
    private void HandleStainCleaned(Zones.CurrentZone zone)
    {
        if (cleanedPerZone.ContainsKey(zone))
            cleanedPerZone[zone]++;
        else
            cleanedPerZone[zone] = 1;
    }

    void Update()
    {
        var currentZone = player != null ? player.currentZone : Zones.CurrentZone.Hall;

        totalStainsPerZone.TryGetValue(currentZone, out int totalInitial);
        totalStainsInZone = totalInitial;

        // Si quieres usar remaining real puedes seguir consultando zoneComponents,
        // pero ahora tenemos el contador de limpias garantizado por el evento.
        cleanedPerZone.TryGetValue(currentZone, out int cleanedCount);
        stainsCleanedInZone = Mathf.Clamp(cleanedCount, 0, (int)totalStainsInZone);

        loadingProgress = (totalStainsInZone > 0f) ? (stainsCleanedInZone / totalStainsInZone) : 1f;

        // Actualizar UI
        if (loadingImage != null)
            loadingImage.fillAmount = loadingProgress;
        if (loadingText != null)
        {
            if (loadingProgress < 1f)
                loadingText.text = Mathf.RoundToInt(loadingProgress * 100f) + "%";
            else
                loadingText.text = "100%";
        }
    }
}

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
    public float loadingProgress = 0;

    public float totalStainsInZone;
    public float stainsCleanedInZone;

    public Image zoneImage;
    public Sprite[] zoneSprites;

    private Dictionary<Zones.CurrentZone, int> totalStainsPerZone = new Dictionary<Zones.CurrentZone, int>();
    private Dictionary<Zones.CurrentZone, int> cleanedPerZone = new Dictionary<Zones.CurrentZone, int>();

    private PlayerMovement player;
    private Zones[] zoneComponents;

    private Zones.CurrentZone lastZone = Zones.CurrentZone.Hall;

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

        var initialZone = player != null ? player.currentZone : Zones.CurrentZone.Hall;
        lastZone = initialZone;
        UpdateZoneImage(initialZone);
    }

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

        if (currentZone != lastZone)
        {
            UpdateZoneImage(currentZone);
            lastZone = currentZone;
        }

        totalStainsPerZone.TryGetValue(currentZone, out int totalInitial);
        totalStainsInZone = totalInitial;

        cleanedPerZone.TryGetValue(currentZone, out int cleanedCount);
        stainsCleanedInZone = Mathf.Clamp(cleanedCount, 0, (int)totalStainsInZone);

        loadingProgress = (totalStainsInZone > 0f) ? (stainsCleanedInZone / totalStainsInZone) : 1f;

        if (loadingImage != null)
            loadingImage.fillAmount = loadingProgress;
    }

    private void UpdateZoneImage(Zones.CurrentZone zone)
    {
        if (zoneImage == null || zoneSprites == null)
            return;

        int index = (int)zone;
        if (index >= 0 && index < zoneSprites.Length && zoneSprites[index] != null)
        {
            zoneImage.sprite = zoneSprites[index];
            zoneImage.enabled = true;
        }
        else
        {
            zoneImage.enabled = false;
        }
    }
}

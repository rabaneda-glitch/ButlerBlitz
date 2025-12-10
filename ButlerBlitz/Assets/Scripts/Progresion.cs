using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Progresion : MonoBehaviour
{
    public Image loadingImage;
    public float loadingProgress = 0;

    public float stainsTotal;
    private float stainsCleaned;

    private Stain stainScript;
    private Stain st;

    void Start()
    {
        stainsTotal = Object.FindObjectsByType<Stain>(FindObjectsSortMode.None).Length;
        stainsCleaned = 0;
        st = Object.FindFirstObjectByType<Stain>();
        Debug.Log(st.gameObject.name);
    }

    void Update()
    {
        loadingImage.fillAmount = loadingProgress;
       
    }

    public void IncrementStainsCleaned()
    {
        stainsCleaned += 1f;
        loadingProgress = (stainsTotal > 0) ? (stainsCleaned / stainsTotal) : 1f;
        loadingImage.fillAmount = loadingProgress;
    }
}

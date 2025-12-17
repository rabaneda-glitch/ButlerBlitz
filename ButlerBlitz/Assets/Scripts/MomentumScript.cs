using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MomentumScript : MonoBehaviour
{
    public static MomentumScript Instance; // Singleton simple

    [Header("Cantidad Inicial")]
    [SerializeField] float qtyMmt = 10f;

    [Header("Decrecimiento")]
    [SerializeField] float decrAlto = 5f;
    [SerializeField] float decrBajo = 2.5f;
    [SerializeField] float mmtMinimo = 2f;

    [Header("Niveles de aumento")]
    public float bajoMmt = 10f;
    public float medioMmt = 20f;
    public float altoMmt = 30f;
    public float mAltoMmt = 40f;
    public float comboMmt = 15f;


    bool isDecreasing = true;
    [NonSerialized] public bool isWalking = false;
    float ultimoNivel = 0f,
    decr;
    public float ActualMomentum;

    private float timeInRange = 0f;
    private float totalTimeInRange90To100 = 0f;
    private bool isInRange90To100 = false;

    void Awake()
    {
        // Asegurar una sola instancia
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        highRange();

        if (isDecreasing)
            qtyMmt -= decr * Time.deltaTime;

        Decrecer();

        if (qtyMmt <= 0)
        {
            //qtyMmt = 0;
            SceneManager.LoadScene("GameOver");
        }
        ActualMomentum = qtyMmt;
    }

    public float highRange()
    {
        if (qtyMmt >= 90f && qtyMmt <= 100f)
        {
            if (!isInRange90To100)
            {
                isInRange90To100 = true;
                timeInRange = 0f; // Reseteamos el contador cuando entramos en el rango
            }
            timeInRange += Time.deltaTime; // Contamos el tiempo mientras estamos en el rango
        }
        else
        {
            // Solo sumamos al tiempo total si hemos estado dentro del rango previamente
            if (isInRange90To100)
            {
                totalTimeInRange90To100 += timeInRange; // Acumulamos el tiempo
                timeInRange = 0f; // Reset del tiempo actual
                isInRange90To100 = false; // Salimos del rango
            }
        }
        
        return totalTimeInRange90To100;
    }

    void Decrecer()
    {
        if (isWalking)
        {
            decr = decrBajo;

            if (qtyMmt <= mmtMinimo)
            {
                qtyMmt = mmtMinimo;

            }
        }
        else
        {
            decr = decrAlto;
            if (qtyMmt <= 0)
            {
                qtyMmt = 0;
            }
        }

    }

    public void Aumentar(float nivel)
    {
        qtyMmt += nivel;

        if (nivel > 10 && ultimoNivel > 10)
        {
            qtyMmt += comboMmt;
            Debug.Log("Combo +15pt");
        }

        ultimoNivel = nivel;

        if (qtyMmt > 100f)
            qtyMmt = 100f;

        if (ScoreScript.Instance != null)
            ScoreScript.Instance.AddPoints(nivel);
        else
            Debug.LogWarning("ScoreScript.Instance is not initialized.");

    }

    public float GetMomentum() => qtyMmt;

    public float GetTotalTimeInRange90To100() => totalTimeInRange90To100;
}





using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance; // Singleton simple

    public TextMeshProUGUI TimerText;
    public float timer = 60;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        TimerText.text = timer.ToString("f0");

        if (timer <= 0)
        {
            timer = 0;
            TimerText.text = "0";

            SceneManager.LoadScene("GameOver");

            enabled = false;
        }
    }

    public float GetTimeLeft() => timer;
}

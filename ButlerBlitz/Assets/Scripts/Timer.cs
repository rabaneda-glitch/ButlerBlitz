using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public float timer = 60;
    public TextMeshProUGUI TimerText;
    
    void Update()
    {
        timer -= Time.deltaTime;
        TimerText.text = "" + timer.ToString("f1");

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}

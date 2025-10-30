using UnityEngine;

public class MomentumScript : MonoBehaviour
{
    float qtyMmt = 100f;

    float decr = 5f;

    float bajoMmt = 10f,
        medioMmt = 20f,
        altoMmt = 30f,
        mAltoMmt = 40f,
        comboMmt = 15f;

    bool isDecreasing = true;

    void Update()
    {
        if (isDecreasing) qtyMmt = qtyMmt - decr * Time.deltaTime;
        

        
        if (decr == 2.5f && qtyMmt <= 10)
        {
            qtyMmt = 10;
            isDecreasing = false;
        }
        if (decr == 5 && qtyMmt <= 0)
        {
            qtyMmt = 0;
            isDecreasing = false;
        }
    }

    void OnGUI()
    {
        string qtyMmtText = qtyMmt.ToString("0.0");

        GUI.Box(new Rect(20, 50, Screen.width - 40, 30), qtyMmtText);

        // Botones de velocidad
        if (GUI.Button(new Rect(20, 100, 100, 50), "5%/s"))
        {
            print("5%/s");
            decr = 5f;
        }
        if (GUI.Button(new Rect(140, 100, 100, 50), "2.5%/s"))
        {
            print("2.5%/s");
            decr = 2.5f;
        }

        //Decreciendo booleana
        if (
            GUI.Button(
                new Rect(Screen.width - 20 - 100, 100, 100, 50),
                isDecreasing ? "Decreciendo" : "No Decreciendo"
            )
        )
        {
            isDecreasing = !isDecreasing;
        }

        //Botones de niveles
        if (GUI.Button(new Rect(20, 170, 100, 50), "Bajo"))
        {
            print("Nivel bajo");
        }
        if (GUI.Button(new Rect(20 + 120, 170, 100, 50), "Medio"))
        {
            print("Nivel medio");
        }
        if (GUI.Button(new Rect(20 + (120 * 2), 170, 100, 50), "Alto"))
        {
            print("Nivel alto");
        }
        if (GUI.Button(new Rect(20 + (120 * 3), 170, 100, 50), "Muy alto"))
        {
            print("Nivel muy alto");
        }
    }
}

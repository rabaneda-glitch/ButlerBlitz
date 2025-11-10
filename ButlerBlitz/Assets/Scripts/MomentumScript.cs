using UnityEngine;

public class MomentumScript : MonoBehaviour
{
    public static MomentumScript Instance; // Singleton simple

    [SerializeField] float qtyMmt = 0f;
    [SerializeField] float decr = 5f;

    public float bajoMmt = 10f,
        medioMmt = 20f,
        altoMmt = 30f,
        mAltoMmt = 40f,
        comboMmt = 15f;

    bool isDecreasing = true;
    float ultimoNivel = 0f;

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
        if (isDecreasing)
            qtyMmt -= decr * Time.deltaTime;

        Decrecer();

        if (qtyMmt <= 0)
        {
            qtyMmt = 0;
            GameOver();
        }
    }

    void Decrecer()
    {
        if (decr == 2.5f && qtyMmt <= 10)
            qtyMmt = 10;
        if (decr == 5 && qtyMmt <= 0)
            qtyMmt = 0;
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
    }

    void GameOver()
    {
        Debug.Log("❌ Momentum = 0 → Game Over");
        // Aquí puedes pausar el juego, cambiar de escena, etc.
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
            Aumentar(bajoMmt);
            print("Nivel bajo");
        }
        if (GUI.Button(new Rect(20 + 120, 170, 100, 50), "Medio"))
        {
            Aumentar(medioMmt);
            print("Nivel medio");
        }
        if (GUI.Button(new Rect(20 + (120 * 2), 170, 100, 50), "Alto"))
        {
            Aumentar(altoMmt);
            print("Nivel alto");
        }
        if (GUI.Button(new Rect(20 + (120 * 3), 170, 100, 50), "Muy alto"))
        {
            Aumentar(mAltoMmt);
            print("Nivel muy alto");
        }
    }

    public float GetMomentum() => qtyMmt;
}


    


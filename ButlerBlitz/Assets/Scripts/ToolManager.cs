using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance { get; private set; }

    //Define los tipos de herramienta que el jugador puede usar
    public enum Tool { Vacuum = 1, Sponge = 2, Duster = 3, Mop = 4 }
    public Tool currentTool = Tool.Vacuum; //Por defecto Vacuum

    [Header("Referencia al script que muestra la herramienta en cámara")]
    public ToolDisplay toolDisplay;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetTool(Tool newTool)
    { //Cambia la herramienta activa a una nueva

        if (currentTool == newTool) return;
        currentTool = newTool;

        // Mostrar la herramienta visualmente
        if (toolDisplay != null)
            toolDisplay.ShowTool(currentTool);

        Debug.Log("Herramienta actual: " + currentTool);
    }

    public bool IsCorrectToolFor(Stain stain)
    { //Comprueba si la herramienta activa es la correcta para el tipo de mancha
        if (stain == null) return false;

        switch (stain.type)
        {
            case Stain.StainType.Dust: return currentTool == Tool.Duster;
            case Stain.StainType.Grease: return currentTool == Tool.Sponge;
            case Stain.StainType.Water: return currentTool == Tool.Mop;
            case Stain.StainType.Mud: return currentTool == Tool.Vacuum;
            default: return false;
        }
    }
}

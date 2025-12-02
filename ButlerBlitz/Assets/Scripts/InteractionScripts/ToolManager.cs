using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance { get; private set; }

    public enum Tool {Vacuum = 0, Sponge = 1, Duster = 2, Mop = 3}

    [Header("Referencias")]
    [SerializeField] public ToolChange toolChange;

    [Header("Inventario")]
    public bool HasKey = false; // Estado lógico para si el jugador tiene la llave.

    private Tool currentTool;
    public Tool CurrentTool => currentTool;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (toolChange == null) return;

        Tool newTool = (Tool)toolChange.SelectedTool;

        if (newTool != currentTool)
        {
            currentTool = newTool;
            Debug.Log($"Herramienta actual: {currentTool}");
        }

        HasKey = toolChange.IsKeyActive;
    }

    public bool IsCorrectToolFor(Stain stain)
    {
        if (stain == null) return false;

        return stain.type switch
        {
            Stain.StainType.Dust => currentTool == Tool.Duster,
            Stain.StainType.Grease => currentTool == Tool.Sponge,
            Stain.StainType.Water => currentTool == Tool.Mop,
            Stain.StainType.Mud => currentTool == Tool.Vacuum,
            _ => false
        };
    }

    // NUEVA FUNCIÓN para verificar si se puede abrir una puerta.
    public bool CanOpenDoor()
    {
        // La puerta se puede abrir si la llave está activa y en la mano.
        return HasKey;
    }
}

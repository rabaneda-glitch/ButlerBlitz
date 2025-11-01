using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance { get; private set; }

    public enum Tool { Vacuum = 1, Sponge = 2, Duster = 3, Mop = 4 }
    public Tool currentTool = Tool.Vacuum;

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
    {
        if (currentTool == newTool) return;
        currentTool = newTool;
        Debug.Log("Herramienta actual: " + currentTool);
    }

    public bool IsCorrectToolFor(Stain stain)
    {
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

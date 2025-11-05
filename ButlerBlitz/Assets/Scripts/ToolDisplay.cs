using UnityEngine;

public class ToolDisplay : MonoBehaviour
{
    [Header("Prefabs de herramientas")]
    public GameObject Vacuum;
    public GameObject Sponge;
    public GameObject Duster;
    public GameObject Mop;

    private ToolManager toolManager;

    void Start()
    {
        toolManager = ToolManager.Instance;
    }

    public void ShowTool(ToolManager.Tool tool)
    {
        GameObject prefab = GetToolPrefab(tool);
        if (prefab != null && toolManager != null)
        {
            toolManager.EquipTool(prefab);
        }
        else
        {
            Debug.LogWarning("No se encontró el prefab o el ToolManager es nulo.");
        }
    }

    public GameObject GetToolPrefab(ToolManager.Tool tool)
    {
        switch (tool)
        {
            case ToolManager.Tool.Vacuum: return Vacuum;
            case ToolManager.Tool.Sponge: return Sponge;
            case ToolManager.Tool.Duster: return Duster;
            case ToolManager.Tool.Mop: return Mop;
            default: return null;
        }
    }
}

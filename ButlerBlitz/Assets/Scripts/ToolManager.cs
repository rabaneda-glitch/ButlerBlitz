using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance { get; private set; }

    public enum Tool { Vacuum = 1, Sponge = 2, Duster = 3, Mop = 4 }
    public Tool currentTool = Tool.Vacuum;

    [Header("Referencias")]
    public ToolDisplay toolDisplay;
    public Transform toolHolder; // punto en la cámara donde van las herramientas

    private GameObject currentToolInstance;

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

        if (toolDisplay != null)
            toolDisplay.ShowTool(currentTool);

        Debug.Log("Herramienta actual: " + currentTool);
    }

    // Este método lo usa ToolDisplay para colocar la herramienta según su GripPoint
    public void EquipTool(GameObject newToolPrefab)
    {
        if (currentToolInstance != null)
            Destroy(currentToolInstance);

        currentToolInstance = Instantiate(newToolPrefab);

        Transform gripPoint = currentToolInstance.transform.Find("GripPoint");

        if (gripPoint != null && toolHolder != null)
        {
            // Primero la hacemos hija temporalmente del ToolHolder
            currentToolInstance.transform.SetParent(toolHolder, false);

            // Ahora aplicamos la alineación para que el GripPoint encaje perfectamente
            currentToolInstance.transform.localPosition = -gripPoint.localPosition;
            currentToolInstance.transform.localRotation = Quaternion.Inverse(gripPoint.localRotation);
        }
        else
        {
            // Fallback: si no tiene GripPoint, simplemente la colocamos al centro
            currentToolInstance.transform.SetParent(toolHolder, false);
            currentToolInstance.transform.localPosition = Vector3.zero;
            currentToolInstance.transform.localRotation = Quaternion.identity;
        }
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

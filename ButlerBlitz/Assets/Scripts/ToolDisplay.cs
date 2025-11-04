using UnityEngine;

public class ToolDisplay : MonoBehaviour
{
    [Header("Referencia al punto donde se mostrará la herramienta")]
    public Transform toolHolder; // Hijo de la cámara

    [Header("Modelos 3D de las herramientas (en orden: Vacuum, Sponge, Duster, Mop)")]
    public GameObject[] toolPrefabs; //Array con los prefabs de las herramientas

    private GameObject currentToolInstance;

    public void ShowTool(ToolManager.Tool tool)
    {
        int toolIndex = (int)tool - 1; // Convierte el valor del enum a un array (0,1,2,3)

        // //Si ya hay una herramienta instanciada, la destruye antes de mostrar la nueva
        if (currentToolInstance != null)
            Destroy(currentToolInstance);

        // Instanciar la nueva herramienta
        if (toolIndex >= 0 && toolIndex < toolPrefabs.Length)
        {
            currentToolInstance = Instantiate(toolPrefabs[toolIndex], toolHolder);
            currentToolInstance.transform.localPosition = Vector3.zero;
            currentToolInstance.transform.localRotation = Quaternion.identity;
        }
    }
}

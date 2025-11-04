using UnityEngine;
using UnityEngine.InputSystem;

public class ToolChange : MonoBehaviour
{
    private ToolControl controls; //Clase generada automáticamente por el InputSystem

    void Awake() //Crea una nueva instancia de los controles definidos en el archivo de Input Actions
    {
        controls = new ToolControl();
    }

    void OnEnable() //Activa controles
    {
        controls.Tools.Enable();
        controls.Tools.ChangeTool.performed += OnChangeTool;
    }

    void OnDisable() //Desactiva controles
    {
        controls.Tools.ChangeTool.performed += OnChangeTool;
        controls.Tools.Disable();
    }

    private void OnChangeTool(InputAction.CallbackContext ctx)
    { // Se ejecuta cada vez que se realiza la acción ChangeTool
        float value = ctx.ReadValue<float>(); //Obtiene valor de entrada

        if (ToolManager.Instance == null) return;

        //Si se usa la rueda del ratón
        if (Mathf.Abs(value) > 0.5f)
        {
            if (value > 0) NextTool();
            else PreviousTool();
        }

        //Si se usan teclas numéricas (1–4)
        if (value >= 1 && value <= 4)
        {
            ToolManager.Instance.SetTool((ToolManager.Tool)(int)value);
        }
    }

    private void NextTool() //Avanza a la siguiente herramienta (si está en 4, vuelve a 1)
    {
        var tool = ToolManager.Instance.currentTool;
        int next = ((int)tool % 4) + 1;
        ToolManager.Instance.SetTool((ToolManager.Tool)next);
    }

    private void PreviousTool() //Retrocede (si está en 1, pasa a 4)
    {
        var tool = ToolManager.Instance.currentTool;
        int prev = (int)tool - 1;
        if (prev < 1) prev = 4;
        ToolManager.Instance.SetTool((ToolManager.Tool)prev);
    }
}

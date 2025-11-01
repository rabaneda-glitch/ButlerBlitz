using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTool: MonoBehaviour
{
    private ToolControl controls;

    void Awake()
    {
        controls = new ToolControl();
    }

    void OnEnable()
    {
        controls.Tools.Enable();
        controls.Tools.ChangeTool.performed += OnChangeTool;
    }

    void OnDisable()
    {
        controls.Tools.ChangeTool.performed -= OnChangeTool;
        controls.Tools.Disable();
    }

    private void OnChangeTool(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();

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

    private void NextTool()
    {
        var tool = ToolManager.Instance.currentTool;
        int next = ((int)tool % 4) + 1;
        ToolManager.Instance.SetTool((ToolManager.Tool)next);
    }

    private void PreviousTool()
    {
        var tool = ToolManager.Instance.currentTool;
        int prev = (int)tool - 1;
        if (prev < 1) prev = 4;
        ToolManager.Instance.SetTool((ToolManager.Tool)prev);
    }
}

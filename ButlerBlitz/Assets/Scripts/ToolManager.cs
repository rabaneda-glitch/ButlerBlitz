using UnityEngine;
using System;

public class ToolManager : MonoBehaviour{
    public static ToolManager Instance { get; private set; }
    public enum Tool { Vacuum = 1, Sponge = 2, Duster = 3, Mop = 4 } //Herramientas disponibles
    [Header("Herramienta inicial")]
    public Tool currentTool = Tool.Vacuum;//Herramienta activa por defecto
    [Header("Input")]
    public bool allowNumberKeys = true;// Permitir cambiar herramienta

    void Start(){ //Comprueba que solo hay un ToolManager en la escena
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    void Update(){
        HandleInput(); // Maneja la entrada del usuario (interacción por teclado)
    }
    private void HandleInput(){
        if (allowNumberKeys)// ambiar herramienta con teclas numéricas
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SetTool(Tool.Vacuum);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SetTool(Tool.Sponge);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SetTool(Tool.Duster);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SetTool(Tool.Mop);
        }
    }
    public void SetTool(Tool t){ //Cambiar herramienta actual
        if (currentTool == t) return;
        currentTool = t;
        Debug.Log("Usando: " + currentTool);
    }
    public bool IsCorrectToolFor(Stain stain){//verifica si la herramienta actual es la correcta
        if (stain == null)
            return false;

        if (stain.type == Stain.StainType.Dust)
            return currentTool == Tool.Duster;
        else if (stain.type == Stain.StainType.Grease)
            return currentTool == Tool.Sponge;
        else if (stain.type == Stain.StainType.Water)
            return currentTool == Tool.Mop;
        else if (stain.type == Stain.StainType.Mud)
            return currentTool == Tool.Vacuum;
        else
            return false;
    }

}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ToolChange : MonoBehaviour
{
    public int SelectedTool = 0;
    private const int CLEANING_TOOL_COUNT = 4;
    private const int MAX_TOOL_INDEX = CLEANING_TOOL_COUNT - 1; // El índice máximo es 3

    [SerializeField] private Animator handPivotAnimator;

    //duración animación
    [SerializeField] private float hideDuration = 0.4f;
    [SerializeField] private float showDuration = 0.4f;

    [SerializeField] private Vector3 handPivotVisibleLocalPosition = Vector3.zero; //pisición inicial

    [Header("Gestión de Llaves")]
    [SerializeField] private GameObject keyHandModel; //GameObject de la Llave
    [HideInInspector] public bool IsKeyActive = false; // Estado para saber si la llave está activa

    [Header("Drop Settings")]
    [SerializeField] private GameObject keyWorldPrefab; // Prefab de la llave del mundo
    [SerializeField] private Transform dropPoint;      // Punto desde donde se suelta la llave

    private bool isChanging = false;

    void Start()
    {
        if (handPivotAnimator != null)
        {
            handPivotAnimator.enabled = false;

            handPivotAnimator.gameObject.transform.localPosition = handPivotVisibleLocalPosition;
        }

        SelectTool();
    }

    void Update()
    {
        if (isChanging) return;

        if (Input.GetKeyDown(KeyCode.V) && IsKeyActive)
        {
            DropKey();
            return; // Detiene la ejecución para que no intente cambiar de herramienta
        }
        if (IsKeyActive) return;  //Si la llave está activa, se ignoran las entradas de la ruleta.

        int previousSelectedTool = SelectedTool;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedTool >= MAX_TOOL_INDEX) // Usamos 3 como límite
                SelectedTool = 0;
            else
                SelectedTool++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedTool <= 0)
                SelectedTool = MAX_TOOL_INDEX; // Usamos 3 como límite
            else
                SelectedTool--;
        }

        if (previousSelectedTool != SelectedTool)
        {
            StartCoroutine(ChangeToolAnimated(previousSelectedTool));
        }
    }

    IEnumerator ChangeToolAnimated(int previousTool)
    {
        isChanging = true;

        if (handPivotAnimator != null)
        {

            handPivotAnimator.enabled = true;

            handPivotAnimator.Play("ToolHide", 0, 0f);
        }

        yield return new WaitForSeconds(hideDuration);

        SelectTool();

        if (handPivotAnimator != null)
        {
            handPivotAnimator.Play("ToolShow", 0, 0f);
        }

        yield return new WaitForSeconds(showDuration);
        transform.localPosition = Vector3.zero;

        if (handPivotAnimator != null)
            handPivotAnimator.enabled = false;

        isChanging = false;
    }

    // ToolChange.cs
    // REEMPLAZA la función SelectTool() existente por esta
    void SelectTool()
    {
        int i = 0;
        foreach (Transform tool in transform)
        {
            // 1. Si la llave está activa, desactiva todas las herramientas de limpieza.
            if (IsKeyActive)
            {
                tool.gameObject.SetActive(false);
            }
            // 2. Si la llave NO está activa, usa la lógica normal de selección.
            else
            {
                if (i == SelectedTool)
                    tool.gameObject.SetActive(true);
                else
                    tool.gameObject.SetActive(false);
            }
            i++;
        }

        // 3. Gestiona la visibilidad de la llave
        if (keyHandModel != null)
        {
            keyHandModel.SetActive(IsKeyActive);
        }
    }

    // ToolChange.cs
    public void SetKeyVisibility(bool visibility)
    {
        // Solo aplica el cambio si el estado es diferente y no hay animación en curso.
        if (IsKeyActive != visibility && !isChanging)
        {
            IsKeyActive = visibility;

            // Llama a la corrutina de animación existente para que la transición sea suave.
            // No importa qué valor le pases a previousTool, ya que SelectTool() ahora maneja la visibilidad.
            StartCoroutine(ChangeToolAnimated(SelectedTool));
        }
    }

    public void DropKey()
    {
        // Verifica si la llave está activa y no hay animación en curso
        if (!IsKeyActive || isChanging) return;

        // 1. Oculta la llave de la mano y muestra la herramienta anterior.
        // Esto llama a ChangeToolAnimated y SetKeyVisibility(false) se maneja internamente.
        SetKeyVisibility(false);

        // 2. Instancia el objeto de la llave en el mundo.
        if (keyWorldPrefab != null && dropPoint != null)
        {
            // Instancia el Prefab en la posición y rotación del Drop Point.
            GameObject droppedKey = Instantiate(keyWorldPrefab, dropPoint.position, dropPoint.rotation);

            if (droppedKey.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                // 1. Asegúrate de que no estás desactivando la gravedad aquí,
                //    y que estás aplicando el impulso para que salga del jugador.
                rb.isKinematic = false; // Debe ser false para que la física funcione.

                rb.AddForce(dropPoint.forward * 2f, ForceMode.Impulse);
                rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("Falta asignar keyWorldPrefab o dropPoint en ToolChange para soltar la llave.");
        }
    }
}

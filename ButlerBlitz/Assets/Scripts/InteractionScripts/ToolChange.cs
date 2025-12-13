using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ToolChange : MonoBehaviour
{
    public int SelectedTool = 0;
    private const int CLEANING_TOOL_COUNT = 4;
    private const int MAX_TOOL_COUNT = CLEANING_TOOL_COUNT + 1; //5 herramientas en total si se recoge la llave
    private const int MAX_TOOL_INDEX = MAX_TOOL_COUNT - 1; //índice máximo es 4  si no se ha recogido la llave 
    public const int KEY_TOOL_INDEX = 4;

    [SerializeField] private Animator handPivotAnimator;

    //duración animación
    [SerializeField] private float hideDuration = 0.4f;
    [SerializeField] private float showDuration = 0.4f;

    [SerializeField] private Vector3 handPivotVisibleLocalPosition = Vector3.zero;

    private bool isChanging = false;

    int i = 0;

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

        int previousSelectedTool = SelectedTool;

        int currentMaxIndex = MAX_TOOL_INDEX;

        if (ToolManager.Instance != null && !ToolManager.Instance.HasKey)
        {
            currentMaxIndex = MAX_TOOL_INDEX - 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedTool >= currentMaxIndex)
            {
                SelectedTool = 0;
            }
            else
            {
                SelectedTool++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedTool <= 0)
            {
                SelectedTool = currentMaxIndex;
            }
            else
            {
                SelectedTool--;
            }
        }

        if (previousSelectedTool != SelectedTool)
        {
            StartCoroutine(ChangeToolAnimated(previousSelectedTool));
        }
    }

    public IEnumerator ChangeToolAnimated(int previousTool)
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

    void SelectTool()
    {
        bool shouldShowKey = false;
        if (ToolManager.Instance != null && ToolManager.Instance.HasKey && (SelectedTool == KEY_TOOL_INDEX))
        {
            shouldShowKey = true;
        }

        int i = 0;
        foreach (Transform tool in transform)
        {
            if (i < CLEANING_TOOL_COUNT)
            {
                bool isCleaningToolSelected = (i == SelectedTool) && !shouldShowKey;
                tool.gameObject.SetActive(isCleaningToolSelected);
            }
            else if (i == KEY_TOOL_INDEX)
            {
                tool.gameObject.SetActive(shouldShowKey);
            }

            i++;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ToolChange : MonoBehaviour
{
    public int SelectedTool = 0;
    private const int CLEANING_TOOL_COUNT = 4;
    private const int MAX_TOOL_INDEX = CLEANING_TOOL_COUNT - 1;

    [SerializeField] private Animator handPivotAnimator;

    [SerializeField] private float hideDuration = 0.4f;
    [SerializeField] private float showDuration = 0.4f;

    [SerializeField] private Vector3 handPivotVisibleLocalPosition = Vector3.zero;

    [Header("Gestión de Llave")]
    [SerializeField] private GameObject keyHandModel;
    [HideInInspector] public bool IsKeyActive = false;

    [Header("Gestión lanzar llave")]
    [SerializeField] private GameObject keyWorldPrefab;
    [SerializeField] private Transform dropPoint;

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
            return; 
        }
        if (IsKeyActive) return;

        int previousSelectedTool = SelectedTool;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedTool >= MAX_TOOL_INDEX)
                SelectedTool = 0;
            else
                SelectedTool++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedTool <= 0)
                SelectedTool = MAX_TOOL_INDEX;
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

    void SelectTool()
    {
        int i = 0;
        foreach (Transform tool in transform)
        {
            if (IsKeyActive)
            {
                tool.gameObject.SetActive(false);
            }

            else
            {
                if (i == SelectedTool)
                    tool.gameObject.SetActive(true);
                else
                    tool.gameObject.SetActive(false);
            }
            i++;
        }

        if (keyHandModel != null)
        {
            keyHandModel.SetActive(IsKeyActive);
        }
    }

    public void SetKeyVisibility(bool visibility)
    {
        if (IsKeyActive != visibility && !isChanging)
        {
            IsKeyActive = visibility;

            StartCoroutine(ChangeToolAnimated(SelectedTool));
        }
    }

    public void DropKey()
    {

        if (!IsKeyActive || isChanging) return;

        SetKeyVisibility(false);

        if (keyWorldPrefab != null && dropPoint != null)
        {

            GameObject droppedKey = Instantiate(keyWorldPrefab, dropPoint.position, dropPoint.rotation);

            if (droppedKey.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = false;

                rb.AddForce(dropPoint.forward * 2f, ForceMode.Impulse);
                rb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
            }
        }
    }
}

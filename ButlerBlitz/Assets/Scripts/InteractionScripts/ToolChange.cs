using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; 

public class ToolChange : MonoBehaviour
{
    public int SelectedTool = 0;

    [SerializeField] private Animator handPivotAnimator;

    //duración aniamción
    [SerializeField] private float hideDuration = 0.4f;
    [SerializeField] private float showDuration = 0.4f;

    [SerializeField] private Vector3 handPivotVisibleLocalPosition = Vector3.zero; //pisición inicial

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

        int previousSelectedTool = SelectedTool;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedTool >= transform.childCount - 1)
                SelectedTool = 0;
            else
                SelectedTool++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedTool <= 0)
                SelectedTool = transform.childCount - 1;
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
            if (i == SelectedTool)
                tool.gameObject.SetActive(true);
            else
                tool.gameObject.SetActive(false);
            i++;
        }
    }
}

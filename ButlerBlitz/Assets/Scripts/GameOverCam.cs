using System.Linq;
using UnityEngine;

public class GameOverCam : MonoBehaviour
{
    public Transform[] views;

    public float viewHoldTime = 3f;
    private int currentViewIndex = 0;
    private float viewTimer = 0f;

    Transform currentView;

    private bool[] visitedIndices;
    private int visitedCount = 0;
    private bool hasVisitedAll = false;

    public bool HasVisitedAll => hasVisitedAll;
    private bool viewsRefreshed = false;

    private Timer timer;

    void Start()
    {
        currentView = transform;
        RefreshViewsFromLayer("CamerasPos");

        timer = UnityEngine.Object.FindFirstObjectByType<Timer>();
    }

    public void RefreshViewsFromLayer(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogWarning($"Layer \"{layerName}\" no encontrada. Asegúrate de que existe en la configuración de Layers.");
            views = new Transform[0];
            ResetVisitedState();
            return;
        }

        var allTransforms = UnityEngine.Object.FindObjectsByType<Transform>(FindObjectsSortMode.None);
        var matched = allTransforms
            .Where(t => t.gameObject.layer == layer)
            .OrderBy(t => t.name)
            .ToArray();

        views = matched;

        if (views.Length > 0)
            currentView = views[0];
        else
            currentView = transform;

        InitVisitedState();
    }

    void Update()
    {
        if (timer != null && timer.timer <= 0) {

            if (!viewsRefreshed)
            {
                RefreshViewsFromLayer("CamerasPos");
                viewsRefreshed = true;
            }


            if (views == null || views.Length == 0) return;

            viewTimer += Time.deltaTime;

            if (viewTimer >= viewHoldTime)
            {
                viewTimer = 0f;

                int nextIndex = (currentViewIndex + 1) % views.Length;
                currentViewIndex = nextIndex;
                currentView = views[currentViewIndex];

                if (visitedIndices != null && !visitedIndices[currentViewIndex])
                {
                    visitedIndices[currentViewIndex] = true;
                    visitedCount++;

                    if (visitedCount >= views.Length)
                    {
                        hasVisitedAll = true;
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (currentView == null) return;

        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * viewHoldTime);

        Vector3 currentAngle = new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * viewHoldTime),
            Mathf.Lerp(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * viewHoldTime),
            Mathf.Lerp(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * viewHoldTime)
            );

        transform.eulerAngles = currentAngle;
    }

    private void InitVisitedState()
    {
        if (views == null || views.Length == 0)
        {
            ResetVisitedState();
            return;
        }

        visitedIndices = new bool[views.Length];
        visitedCount = 0;
        hasVisitedAll = false;

        currentViewIndex = 0;
        visitedIndices[currentViewIndex] = true;
        visitedCount = 1;

        if (visitedCount >= views.Length)
            hasVisitedAll = true;
    }

    private void ResetVisitedState()
    {
        visitedIndices = null;
        visitedCount = 0;
        hasVisitedAll = false;
    }

}

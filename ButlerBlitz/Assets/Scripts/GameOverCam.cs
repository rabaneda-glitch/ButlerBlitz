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
    private MomentumScript momentumScript;

    void Start()
    {
        currentView = transform;
        RefreshViewsFromLayer("CamaraPos");

        timer = UnityEngine.Object.FindFirstObjectByType<Timer>();
        momentumScript = UnityEngine.Object.FindFirstObjectByType<MomentumScript>();
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
        if (timer.timer <= 0 || momentumScript.qtyMmt <= 0)
        {

            if (!viewsRefreshed)
            {
                RefreshViewsFromLayer("CamaraPos");
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

    public void NextViewManual()
    {
        if (views == null || views.Length == 0) return;

        currentViewIndex = (currentViewIndex + 1) % views.Length;
        currentView = views[currentViewIndex];
        viewTimer = 0f;

        EnsureVisitedArray();
        MarkVisited(currentViewIndex);
    }

    public void PreviousViewManual()
    {
        if (views == null || views.Length == 0) return;

        currentViewIndex = (currentViewIndex - 1 + views.Length) % views.Length;
        currentView = views[currentViewIndex];
        viewTimer = 0f;

        EnsureVisitedArray();
        MarkVisited(currentViewIndex);
    }

    // Asegura que visitedIndices exista y tenga el tamaño correcto
    private void EnsureVisitedArray()
    {
        if (views == null)
            return;

        if (visitedIndices == null || visitedIndices.Length != views.Length)
        {
            bool[] old = visitedIndices;
            visitedIndices = new bool[views.Length];

            // Si existía un array antiguo, intentar conservar marcas por InstanceID coincidentes
            if (old != null && old.Length > 0)
            {
                int copyCount = Mathf.Min(old.Length, visitedIndices.Length);
                for (int i = 0; i < copyCount; i++)
                    visitedIndices[i] = old[i];
            }

            // Recalcular visitedCount
            visitedCount = visitedIndices.Count(b => b);
            hasVisitedAll = (views.Length > 0) && (visitedCount >= views.Length);
        }
    }

    private void MarkVisited(int idx)
    {
        if (visitedIndices == null || idx < 0 || idx >= visitedIndices.Length) return;

        if (!visitedIndices[idx])
        {
            visitedIndices[idx] = true;
            visitedCount++;
        }

        hasVisitedAll = (views != null && visitedCount >= views.Length);
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CleanerRay : MonoBehaviour
{
    private Camera _camera;
    public float maxDistance = 100f;
    public bool useMousePointer = true;
    private float cooldown = 0f;
    public float cooldownSecs = 0.5f;

    [Header("Layer para manchas")]
    public LayerMask stainLayer; // Solo impactará objetos en este layer

    void Start()
    {
        _camera = GetComponent<Camera>();
        if (_camera == null) _camera = Camera.main;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;

        if (cooldown <= 0 && Input.GetMouseButtonDown(0))
        {
            Ray ray;
            if (useMousePointer)
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                Vector3 center = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f, 0);
                ray = _camera.ScreenPointToRay(center);
            }

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 1.0f);

            RaycastHit hit;

            // Aplicamos el LayerMask usando el parámetro 'layerMask'
            if (Physics.Raycast(ray, out hit, maxDistance, stainLayer, QueryTriggerInteraction.Collide))
            {
                Stain stain = hit.transform.GetComponent<Stain>();
                if (stain == null) stain = hit.transform.GetComponentInParent<Stain>();
                if (stain == null) stain = hit.transform.GetComponentInChildren<Stain>();

                if (stain != null)
                {
                    if (ToolManager.Instance != null)
                    {
                        if (ToolManager.Instance.IsCorrectToolFor(stain))
                        {
                            stain.Clean();
                        }
                        else
                        {
                            Debug.Log("Herramienta incorrecta. Necesitas: " + GetRequiredToolFor(stain.type));
                        }
                    }
                    else
                    {
                        stain.Clean();
                    }
                }
                else
                {
                    Debug.Log("El objeto impactado no contiene Stain");
                }
            }
            else
            {
                Debug.Log("Raycast no golpeó nada");
            }

            cooldown = cooldownSecs;
        }
    }

    private string GetRequiredToolFor(Stain.StainType type)
    {
        switch (type)
        {
            case Stain.StainType.Dust: return "Duster (3)";
            case Stain.StainType.Grease: return "Sponge (2)";
            case Stain.StainType.Water: return "Mop (4)";
            case Stain.StainType.Mud: return "Vacuum (1)";
            default: return "Vacuum";
        }
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CleanerRay : MonoBehaviour
{
    [Header("References")]
    public Camera sourceCamera; // si se deja vacío, usa Camera.main o el Camera del GameObject
    public Transform playerOrientation; // opcional: referencia al orientation del player

    [Header("Ray settings")]
    public float maxDistance = 100f;
    public bool useMousePointer = true; // si false, dispara desde centro de la cámara
    public bool usePlayerForward = false; // si true, ignora cámara y usa playerOrientation.forward

    [Header("Cooldown")]
    public float cooldownSecs = 0.5f;
    private float cooldownTimer = 0f;

    [Header("Layer para manchas")]
    public LayerMask stainLayer; // layer donde están las manchas

    void Start()
    {
        if (sourceCamera == null)
        {
            // intenta obtener la cámara en este objeto (si el script está en el objeto cámara)
            sourceCamera = GetComponent<Camera>();
            if (sourceCamera == null)
                sourceCamera = Camera.main;
        }
    }

    void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f && Input.GetMouseButtonDown(0))
        {
            Ray ray;
            if (usePlayerForward && playerOrientation != null)
            {
                // Rayo desde la posición del player (o cámara) hacia la dirección del orientation
                Vector3 origin = sourceCamera != null ? sourceCamera.transform.position : playerOrientation.position;
                ray = new Ray(origin, playerOrientation.forward);
            }
            else
            {
                if (useMousePointer && sourceCamera != null)
                {
                    ray = sourceCamera.ScreenPointToRay(Input.mousePosition);
                }
                else if (sourceCamera != null)
                {
                    Vector3 center = new Vector3(sourceCamera.pixelWidth / 2f, sourceCamera.pixelHeight / 2f, 0f);
                    ray = sourceCamera.ScreenPointToRay(center);
                }
                else
                {
                    // fallback: usa forward del playerOrientation o del transform
                    Transform t = playerOrientation ? playerOrientation : transform;
                    ray = new Ray(t.position, t.forward);
                }

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 1.0f);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, stainLayer, QueryTriggerInteraction.Collide))
            {
                Stain stain = hit.transform.GetComponent<Stain>()
                              ?? hit.transform.GetComponentInParent<Stain>()
                              ?? hit.transform.GetComponentInChildren<Stain>();

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

            cooldownTimer = cooldownSecs;
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

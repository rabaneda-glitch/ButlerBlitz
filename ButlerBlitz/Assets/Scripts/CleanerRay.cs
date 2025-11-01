//using System.Collections;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CleanerRay : MonoBehaviour
{
    //Variables privadas
    private Camera _camera;              // Cámara desde la que se lanza el rayo
    private bool hasClicked = false;     // Control de clic
    public float maxDistance = 100f;     // Distancia máxima del rayo

    //Parámetros de control del cursor
    public bool useMousePointer = true;  // Si es true, usa el puntero del ratón
    private float cooldown = 0f;
    public float cooldownSecs = 0.5f;

    void Start()
    {
        // Inicialización de la cámara
        _camera = GetComponent<Camera>();
        if (_camera == null) _camera = Camera.main;

        // Configuración del cursor (visible para limpiar)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime; //Tiempo de espera entre clics

        if (cooldown <= 0)
        {
            
            if (Input.GetMouseButtonDown(0)) //Cuando se clica
            {
                hasClicked = true;
 
                Ray ray; // Crea el rayo desde el puntero o el centro de la pantalla
                if (useMousePointer)
                {
                    ray = _camera.ScreenPointToRay(Input.mousePosition);
                }
                else
                {
                    Vector3 center = new Vector3(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f, 0);
                    ray = _camera.ScreenPointToRay(center);
                }

                // Dibuja el rayo para depuración
                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 1.0f);

               
                RaycastHit hit; // Comprueba si hay colisiones con el rayo
                if (Physics.Raycast(ray, out hit, maxDistance, ~0, QueryTriggerInteraction.Collide))
                {
                    // Busca componente Stain
                    Stain stain = hit.transform.GetComponent<Stain>();
                    if (stain == null) stain = hit.transform.GetComponentInParent<Stain>();
                    if (stain == null) stain = hit.transform.GetComponentInChildren<Stain>();


                    if (stain != null) //Si hay mancha, limpia con la herramienta correcta
                    {
                        if (ToolManager.Instance != null)
                        {
                            if (ToolManager.Instance.IsCorrectToolFor(stain))
                            {
                                stain.Clean(); // Limpia la mancha
                            }
                            else
                            {
                                Debug.Log("Herramienta incorrecta. Necesitas: " + GetRequiredToolFor(stain.type));
                            }
                        }
                        else
                        {   
                            stain.Clean(); // Si no hubiera ToolManager, limpia igualmente
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
    }

    private string GetRequiredToolFor(Stain.StainType type) //Indica la herramienta necesaria para cada tipo de mancha
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

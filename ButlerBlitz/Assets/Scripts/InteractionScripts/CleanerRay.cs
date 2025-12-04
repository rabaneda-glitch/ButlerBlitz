using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CleanerRay : MonoBehaviour
{
    private Camera _camera;

    [Header("Ray Settings")]
    [SerializeField] private float maxDistance = 1000f;

    [Header("Cooldown")]
    [SerializeField] private float cooldownSecs = 1f;
    private float cooldown = 0f;

    [Header("FOV (Zoom)")]
    [SerializeField] private float fovZoom = 20f;
    private float fovOriginal;
    private float fovVelocity = 0f;

    void Start()
    {
        _camera = GetComponent<Camera>();
        fovOriginal = _camera.fieldOfView;

    }

    void Update()
    {
        float targetFov = Input.GetMouseButton(1) ? fovZoom : fovOriginal;
        _camera.fieldOfView = Mathf.SmoothDamp(_camera.fieldOfView, targetFov, ref fovVelocity, 0.5f);

        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
            cooldown = cooldownSecs;
        }
    }

    private void ShootRay()
    {
        Vector3 center = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(center);
        RaycastHit hit;

        int interactableMask = LayerMask.GetMask("Stain", "Key", "Door");

        if (Physics.Raycast(ray, out hit, maxDistance, interactableMask))
        {
            KeyPickup key = hit.transform.GetComponent<KeyPickup>();
            if (key != null)
            {
                key.PickUp(); // Llama al método para recogerla
                return; // Detiene la ejecución aquí para que no intente limpiar la llave
            }

            Door door = hit.transform.GetComponent<Door>();
            if (door != null)
            {
                door.Interact(); // Llama al nuevo método Interact() de la puerta
                return; // Detiene la ejecución
            }

            Stain stain = hit.transform.GetComponent<Stain>();
            if (stain != null)
            {
                stain.Interact(null);
            }
        }
    }

   
}

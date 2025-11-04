using UnityEngine;

public class Stain : MonoBehaviour
{
    public enum StainType { Mud, Dust, Grease, Water } // Tipos de manchas
    public StainType type = StainType.Mud; // Tipo de mancha por defecto

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Clean()
    {
        _renderer.enabled = false;
        Destroy(gameObject, 0.1f); //Tiempo que tarda en desaparecer la mancha
    }
}

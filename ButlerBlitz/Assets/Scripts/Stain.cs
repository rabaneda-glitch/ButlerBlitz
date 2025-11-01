using UnityEngine;

public class Stain : MonoBehaviour{
    public enum StainType { Mud, Dust, Grease, Water } // Tipos de manchas
    public StainType type = StainType.Mud; // Tipo de mancha por defecto

    private Renderer _renderer;

    void Start(){
        _renderer = GetComponent<Renderer>(); 
    }

    public void Clean(){
        Debug.Log("Mancha limpia: " + gameObject.name); // Mensaje de la macha limpia
        _renderer.enabled = false;
        Destroy(gameObject, 0.1f); //tiempo que tarda en desaparecer la mancha
    }
}

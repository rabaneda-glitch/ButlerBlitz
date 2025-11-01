using UnityEngine;

public class StainSpawner : MonoBehaviour{ 

    [SerializeField] private GameObject stainPrefab;// Prefab de la mancha que clona
    [SerializeField] private int stainCount = 6; // Número de manchas a generar 
    [SerializeField] private Vector3 areaSize = new Vector3(8, 2, 8); // Tamaño del área donde se generan las manchas

    void Update(){
        if (stainPrefab == null) return;

        for (int i = 0; i < stainCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2),
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );// Posición aleatoria dentro del área
            
            Instantiate(stainPrefab, pos, Quaternion.Euler(90, 0, 0));//Gira las manchas 90 en el eje X para que queden planas
        }
    }

}

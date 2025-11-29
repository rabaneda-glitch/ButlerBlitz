using UnityEngine;
using static PlayerMovement;

public class Zones : MonoBehaviour
{
    public CurrentZone zone;
    public enum CurrentZone
    {
        Hall,
        Lounge,
        Kitchen,
        MasterBedroom,
        Bathroom,
        Library,
        ButlerBedroom,
        Study,
    }

    public float stainsInTheZone;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

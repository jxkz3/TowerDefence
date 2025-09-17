using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isOccupied = false;
    [HideInInspector] public TowerPlacer placer;
    [HideInInspector] public GameObject currentTower; // store spawned tower
}

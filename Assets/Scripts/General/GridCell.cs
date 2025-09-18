using UnityEngine;

public class GridCell : MonoBehaviour
{
    [HideInInspector] public TowerPlacer placer;
    [HideInInspector] public GameObject currentTower;

    public bool isOccupied
    {
        get { return currentTower != null; } // occupied if there’s a tower
    }
}

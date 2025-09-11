using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isOccupied = false;
    [HideInInspector] public TowerPlacer placer; // set by TowerPlacer when clicked
}

using UnityEngine;

public class WallGrid : MonoBehaviour
{
    [HideInInspector] public WallPlacer placer;
    [HideInInspector] public GameObject currentWall;  //  renamed to currentWall

    public bool isOccupied
    {
        get { return currentWall != null; } // occupied if there’s a wall
    }
}

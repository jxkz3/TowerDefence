using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;   // 0 = Red, 1 = Blue
    public GameObject selectionPanel;   // assign your UI panel in Inspector

    private Camera mainCam;
    private GridCell selectedCell;      // the cell player tapped

    void Start()
    {
        mainCam = Camera.main;
        selectionPanel.SetActive(false); // hide panel initially
    }

    void Update()
    {
        // Mouse input (PC)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            CheckCell(Mouse.current.position.ReadValue());

        // Touch input (Mobile)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            CheckCell(Touchscreen.current.primaryTouch.position.ReadValue());
    }

    void CheckCell(Vector2 screenPos)
    {
        Ray ray = mainCam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridCell cell = hit.collider.GetComponent<GridCell>();

            if (cell != null && !cell.isOccupied)
            {
                selectedCell = cell;
                selectionPanel.SetActive(true); // open the tower choice panel
            }
        }
    }

    // Called by Red Button
    public void PlaceRedTower()
    {
        PlaceTower(0); // index 0 = red
    }

    // Called by Blue Button
    public void PlaceBlueTower()
    {
        PlaceTower(1); // index 1 = blue
    }

    // Called By Yellow Tower
    public void PlaceYellowTower()
    {
        PlaceTower(2); // index 2 = yellow
    }


    void PlaceTower(int index)
    {
        if (selectedCell == null || selectedCell.isOccupied) return;

        Instantiate(towerPrefabs[index], selectedCell.transform.position, Quaternion.identity);
        selectedCell.isOccupied = true;
        selectionPanel.SetActive(false); // hide after placing
        selectedCell = null; // clear selection
    }
}

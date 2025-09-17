using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;   // 0 = Red, 1 = Blue, 2 = Yellow
    public int[] towerCosts;            // must match size/order of towerPrefabs
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
        PlaceTower(0); // Red = index 0
    }

    // Called by Blue Button
    public void PlaceBlueTower()
    {
        PlaceTower(1); // Blue = index 1
    }

    // Called by Yellow Tower
    public void PlaceYellowTower()
    {
        PlaceTower(2); // Yellow = index 2
    }

    void PlaceTower(int index)
    {
        if (selectedCell == null) return;

        int cost = towerCosts[index];

        // If cell already has a tower, remove it (sell or swap)
        if (selectedCell.isOccupied && selectedCell.currentTower != null)
        {
            // Optional: refund some coins before replacing
            // CoinManager.Instance.AddCoins(towerCosts[index] / 2); 

            Destroy(selectedCell.currentTower);
            selectedCell.isOccupied = false;
        }

        // Now check if we can afford new tower
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoins(cost))
        {
            GameObject newTower = Instantiate(
                towerPrefabs[index],
                selectedCell.transform.position,
                Quaternion.identity
            );

            selectedCell.currentTower = newTower; // store reference
            selectedCell.isOccupied = true;
            selectionPanel.SetActive(false);
            selectedCell = null;
        }
        else
        {
            Debug.Log("Not enough coins to place " + towerPrefabs[index].name);
        }
    }
}

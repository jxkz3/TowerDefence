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

            if (cell != null) // allow both empty + occupied
            {
                selectedCell = cell;
                selectionPanel.SetActive(true); // open tower choice panel
            }
        }
    }

    // Called by Red Button
    public void PlaceRedTower() => PlaceTower(0);
    public void PlaceBlueTower() => PlaceTower(1);
    public void PlaceYellowTower() => PlaceTower(2);

    void PlaceTower(int index)
    {
        if (selectedCell == null) return;

        int cost = towerCosts[index];

        // Check if player has enough coins
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoins(cost))
        {
            // Destroy old tower if exists
            if (selectedCell.currentTower != null)
            {
                Destroy(selectedCell.currentTower);
            }

            // Place new tower
            GameObject newTower = Instantiate(
                towerPrefabs[index],
                selectedCell.transform.position,
                Quaternion.identity
            );

            selectedCell.currentTower = newTower;

            // Close UI
            selectionPanel.SetActive(false);
            selectedCell = null;
        }
        else
        {
            Debug.Log("Not enough coins to place " + towerPrefabs[index].name);
        }
    }
}

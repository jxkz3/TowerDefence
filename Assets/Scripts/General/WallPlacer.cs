using UnityEngine;
using UnityEngine.InputSystem;

public class WallPlacer : MonoBehaviour
{
    public GameObject[] wallPrefabs;   // 0 = Red, 1 = Blue, 2 = Yellow
    public int[] wallCosts;            // must match size/order of wallPrefabs
    public GameObject selectionPanel;  // assign your UI panel in Inspector

    private Camera mainCam;
    private WallGrid selectedCell;     // the cell player tapped

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
            WallGrid cell = hit.collider.GetComponent<WallGrid>();

            if (cell != null) // allow both empty + occupied
            {
                selectedCell = cell;
                selectionPanel.SetActive(true); // open wall choice panel
            }
        }
    }

    // Called by Red Button
    public void PlaceRedWall() => PlaceWall(0);
    public void PlaceBlueWall() => PlaceWall(1);
    public void PlaceYellowWall() => PlaceWall(2);

    void PlaceWall(int index)
    {
        if (selectedCell == null) return;

        int cost = wallCosts[index];

        // Check if player has enough coins
        if (CoinManager.Instance != null && CoinManager.Instance.SpendCoins(cost))
        {
            // Destroy old wall if exists
            if (selectedCell.currentWall != null)
            {
                Destroy(selectedCell.currentWall);
            }

            // Place new wall
            GameObject newWall = Instantiate(
                wallPrefabs[index],
                selectedCell.transform.position,
                Quaternion.identity
            );

            selectedCell.currentWall = newWall;

            // Close UI
            selectionPanel.SetActive(false);
            selectedCell = null;
        }
        else
        {
            Debug.Log("Not enough coins to place " + wallPrefabs[index].name);
        }
    }
}

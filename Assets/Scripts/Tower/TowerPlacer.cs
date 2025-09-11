using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;   // Assign all tower prefabs in Inspector
    private GameObject selectedTowerPrefab;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;

        // Default tower = first one
        if (towerPrefabs.Length > 0)
            selectedTowerPrefab = towerPrefabs[0];
    }

    public void SelectTower(int index)
    {
        if (index >= 0 && index < towerPrefabs.Length)
        {
            selectedTowerPrefab = towerPrefabs[index];
            Debug.Log("Selected Tower: " + selectedTowerPrefab.name);
        }
    }

    void Update()
    {
        if (selectedTowerPrefab == null) return;

        // Mouse input (PC)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            TryPlaceTower(Mouse.current.position.ReadValue());

        // Touch input (Mobile)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            TryPlaceTower(Touchscreen.current.primaryTouch.position.ReadValue());
    }

    void TryPlaceTower(Vector2 screenPos)
    {
        Ray ray = mainCam.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridCell cell = hit.collider.GetComponent<GridCell>();

            if (cell != null && !cell.isOccupied)
            {
                Instantiate(selectedTowerPrefab, cell.transform.position, Quaternion.identity);
                cell.isOccupied = true;
            }
        }
    }
}

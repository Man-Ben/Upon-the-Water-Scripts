using UnityEngine;

public class BuildingRules : MonoBehaviour
{
    public static BuildingRules Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool IsCellFree(Vector3Int cell)
    {
        if(BuildManager.Instance.OccupiedCells.ContainsKey(cell))
        {
            return false;
        }

        return true;
    }

    public bool HasNeighbour(Vector3Int position)
    {
        Vector3Int[] neighbours =
        {
            position + Vector3Int.back,
            position + Vector3Int.forward,
            position + Vector3Int.left,
            position + Vector3Int.right
        };

        foreach (Vector3Int neighbour in neighbours)
        {
            if(BuildManager.Instance.OccupiedCells.ContainsKey(neighbour))
                return true;
        }

        return false;
    }

    public bool HasRaftBelow(Vector3Int position)
    {
        return BuildManager.Instance.OccupiedCells.ContainsKey(position + Vector3Int.down);
    }

    public bool HasEnoughResource(Category category, int index)
    {
        bool isEnough = true;

        for(int i = 0; i < 6; i++)
            if(PlayerDataManager.Instance.playersInventory.playerInventory[i] - BuildConfigReader.Instance.buildingCatalog[category][index].cost[i] < 0)
            {
                InventoryManager.Instance.ChangeTextColor(i, Color.red);
                isEnough = false;
            }
            else
                InventoryManager.Instance.ChangeTextColor(i, Color.green);
        
        return isEnough;
    }
}

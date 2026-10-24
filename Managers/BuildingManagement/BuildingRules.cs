using UnityEngine;

public class BuildingRules : MonoBehaviour
{
    public static BuildingRules Instance {get; private set;}

    BuildManager buildManager;

    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        buildManager = BuildManager.Instance;
    }

    void InitializeSingleton()
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
        if(buildManager.OccupiedCells.ContainsKey(cell))
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
            if(buildManager.OccupiedCells.ContainsKey(neighbour))
                return true;
        }

        return false;
    }

    public bool HasRaftBelow(Vector3Int position)
    {
        return buildManager.OccupiedCells.ContainsKey(position + Vector3Int.down);
    }

    public bool HasEnoughResource(Category category, int index)
    {
        bool isEnough = true;

        for(int i = 0; i < 6; i++)
        {
            int cost = BuildConfigReader.Instance.buildingCatalog[category][index].cost[i];

            if(cost == 0)
                continue;

            InventoryManager inventoryManager = InventoryManager.Instance;

            int inventoryValue = PlayerDataManager.Instance.playersInventory.playerInventory[i];
            
            if(inventoryValue - cost < 0)
            {
                inventoryManager.ChangeTextColor(i, Color.red);
                isEnough = false;
            }
            else
                inventoryManager.ChangeTextColor(i, Color.green);
            
        }
        
        return isEnough;
    }
}

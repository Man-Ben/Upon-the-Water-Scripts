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
}

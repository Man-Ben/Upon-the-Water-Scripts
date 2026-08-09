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
}

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] public Dictionary<Vector3Int, GameObject> OccupiedCells = new();

    public static BuildManager Instance {get; private set;}

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadRaft();
    }

    void LoadRaft()
    {
        JsonManager.Instance.ReadBuildingStats("SimpleTile");

        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
            {
                Vector3Int placePosition = new Vector3Int(i * 3, 3 , j * 3);

                Instantiate(BuildingCategories.Instance.raftTiles[0], placePosition, Quaternion.identity);
                
                OccupiedCells.Add(new Vector3Int(i, 3, j), BuildingCategories.Instance.raftTiles[0]);
            }
    }



    public bool CanPlace(GameObject guideObject, Category category, int index)
    {
        for(int i = 0; i < JsonManager.Instance.buildingCatalog[category][index].footprint.x; i++)
        {
            for(int j = 0; j < JsonManager.Instance.buildingCatalog[category][index].footprint.y; j++)
            {
                Vector3Int cell = BuildGuideManager.Instance.gridPosition + new Vector3Int(i, 0, j);

                if(!BuildingRules.Instance.IsCellFree(cell))
                {
                    BuildGuideManager.Instance.ChangeColor(guideObject, Color.red);
                    return false;
                }
            } 
        }

        BuildGuideManager.Instance.ChangeColor(guideObject, Color.green);
        return true;
    }

    public void PlaceBuilding(GameObject guideObject, GameObject buildingToPlace)
    {
        Vector3 position = new Vector3(guideObject.transform.position.x, 3, guideObject.transform.position.z);

        Instantiate(buildingToPlace, position, guideObject.transform.rotation);
        
        OccupiedCells.Add(BuildGuideManager.Instance.gridPosition, buildingToPlace);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance {get; private set;}

    
    BuildingCategories buildingCategories;
    BuildConfigReader buildConfig;
    BuildGuideManager buildGuideManager;
    BuildingRules buildingRules;


    public Dictionary<Vector3Int, (Category category, int index)> OccupiedCells = new();


    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        InitializeInstances();
        LoadRaft();
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

    void InitializeInstances()
    {
        buildConfig = BuildConfigReader.Instance;
        buildingCategories = BuildingCategories.Instance;
        buildGuideManager = BuildGuideManager.Instance;
        buildingRules = BuildingRules.Instance;
    }

    void LoadRaft()
    {
        int chunkSize = 4;
        int baseHeight = 2;
        int baseRaft = 2;

        for(int i = 0; i < baseRaft; i++)
            for(int j = 0; j < baseRaft; j++)
            {
                Vector3Int placePosition = new Vector3Int(i * chunkSize, baseHeight, j * chunkSize);

                Instantiate(buildingCategories.raftTiles[0], placePosition, Quaternion.identity);
                
                OccupiedCells.Add(new Vector3Int(i, baseHeight, j), (Category.RaftTile, 0));
            }
    }

    public bool CanPlace(GameObject guideObject, Category category, int index)
    {

        if(!buildingRules.HasEnoughResource(category, index))
        {
            buildGuideManager.ChangeColor(guideObject, Color.red);
            return false;
        }

        int footprintX = buildConfig.buildingCatalog[category][index].footprint.x;
        int footprintY = buildConfig.buildingCatalog[category][index].footprint.y;

        for(int i = 0; i < footprintX; i++)
        {
            for(int j = 0; j < footprintY; j++)
            {
                Vector3Int cell = buildGuideManager.gridPosition + new Vector3Int(i, 0, j);

                if(category == Category.RaftTile)
                {
                    if(!buildingRules.HasNeighbour(cell))
                    {
                        buildGuideManager.ChangeColor(guideObject, Color.red);
                        return false;
                    }
                }
                else
                    if(!buildingRules.HasRaftBelow(cell))
                    {
                        buildGuideManager.ChangeColor(guideObject, Color.red);
                        return false;
                    }


                if(!buildingRules.IsCellFree(cell))
                {
                    buildGuideManager.ChangeColor(guideObject, Color.red);
                    return false;
                }
            } 
        }

        buildGuideManager.ChangeColor(guideObject, Color.green);
        return true;
    }

    public void PlaceBuilding(GameObject guideObject, GameObject buildingToPlace, Category category, int index)
    {
        float x = guideObject.transform.position.x;
        float z = guideObject.transform.position.z;

        int buildingLayer = buildConfig.buildingCatalog[category][index].buildingLayer;

        Vector3 position = new Vector3(x, 2 + buildingLayer, z);

        Instantiate(buildingToPlace, position, guideObject.transform.rotation);
        
        int footprintX = buildConfig.buildingCatalog[category][index].footprint.x; 
        int footprintY = buildConfig.buildingCatalog[category][index].footprint.y; 

        for(int i = 0; i < footprintX; i++)
            for(int j = 0; j < footprintY; j++)
            {
                OccupiedCells.Add(buildGuideManager.gridPosition + new Vector3Int(i, 0, j), (category, index));
            }
    }
}

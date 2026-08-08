using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    Dictionary<Vector3Int, GameObject> raftTiles = new();

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
    
    void Update()
    {
              
    }

    void LoadRaft()
    {
        JsonManager.Instance.ReadBuildingStats("SimpleTile");

        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
            {
                Vector3Int position = new Vector3Int(i * JsonManager.Instance.buildingStats.dimensions.length, 3 , j * JsonManager.Instance.buildingStats.dimensions.width);

                Instantiate(BuildingCategories.Instance.raftTiles[0], position, quaternion.identity);
                
                raftTiles.Add(new Vector3Int(i, 3, j), BuildingCategories.Instance.raftTiles[0]);
            }
    }



    public bool CanPlace(GameObject guideObject)
    {
        if(raftTiles.ContainsKey(BuildGuideManager.Instance.gridPosition))
        {
            BuildGuideManager.Instance.ChangeColor(guideObject, Color.red);
            return false;
        }

        return true;
    }

    public void PlaceBuilding(GameObject guideObject, GameObject buildingToPlace)
    {
        Vector3 position = new Vector3(guideObject.transform.position.x, 3, guideObject.transform.position.z);

        Instantiate(buildingToPlace,  position, guideObject.transform.rotation);
        
        raftTiles.Add(BuildGuideManager.Instance.gridPosition, buildingToPlace);
    }
}

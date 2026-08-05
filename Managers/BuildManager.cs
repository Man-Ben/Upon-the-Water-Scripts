using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    Dictionary<Vector3Int, GameObject> raftTiles = new();

    public static BuildManager Instance {get; private set;}

    void Awake()
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
        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
            {
                Vector3Int position = new Vector3Int(i * Raft.Instance.simpleTileSize, 3, j * Raft.Instance.simpleTileSize);

                Instantiate(Raft.Instance.simpleRaftTile, position, quaternion.identity);
                
                raftTiles.Add(new Vector3Int(i, 3, j), Raft.Instance.simpleRaftTile);
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

    public void PlaceBuilding(GameObject guideObject)
    {
        Vector3 position = new Vector3(guideObject.transform.position.x, 3, guideObject.transform.position.z);

        Instantiate(Raft.Instance.simpleRaftTile,  position, guideObject.transform.rotation);
        
        raftTiles.Add(BuildGuideManager.Instance.gridPosition, Raft.Instance.simpleRaftTile);
    }
}

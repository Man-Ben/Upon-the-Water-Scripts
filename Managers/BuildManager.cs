using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    Dictionary<Vector3, GameObject> raftTiles = new();


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

        PlaceGuideObject();
    }

    void LoadRaft()
    {
        for(int i = 0; i < 5; i++)
            for(int j = 0; j < 5; j++)
            {
                Vector3 position = new Vector3(i * Raft.Instance.simpleTileSize, 3, j * Raft.Instance.simpleTileSize);

                Instantiate(Raft.Instance.simpleRaftTile, position, quaternion.identity);
                
                raftTiles.Add(position, Raft.Instance.simpleRaftTile);
            }
        
    }

    public void PlaceGuideObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Instantiate(Raft.Instance.simpleRaftTile, mousePosition, quaternion.identity);
    }

}

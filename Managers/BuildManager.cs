using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    Dictionary<Vector3, GameObject> raftTiles = new();

    GameObject guideObject;

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
        if(guideObject != null)
        {
            BuildGuideManager.Instance.FollowMouse(guideObject);
        
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(Raft.Instance.simpleRaftTile, guideObject.transform.position, guideObject.transform.rotation);
            }

            if(Input.GetMouseButtonDown(1))
            {
                Destroy(guideObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.B) && guideObject == null)
        {
            guideObject = PlaceGuideObject();

            BuildGuideManager.Instance.ChangeColor(guideObject);
        }
        
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

    public GameObject PlaceGuideObject()
    {
        return Instantiate(Raft.Instance.simpleRaftTile);        
    }

}

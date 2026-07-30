using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    Dictionary<Vector3Int, GameObject> raftTiles = new();

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
        
            if(CanPlace())
            {
                BuildGuideManager.Instance.ChangeColor(guideObject, Color.green);

                if(Input.GetMouseButtonDown(0))
                {
                    Instantiate(Raft.Instance.simpleRaftTile,  new Vector3(guideObject.transform.position.x, 3, guideObject.transform.position.z), guideObject.transform.rotation);
                    raftTiles.Add(BuildGuideManager.Instance.gridPosition, Raft.Instance.simpleRaftTile);
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                Destroy(guideObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.B) && guideObject == null)
        {
            guideObject = PlaceGuideObject();

            guideObject.GetComponent<Collider>().enabled = false;

           // BuildGuideManager.Instance.ChangeColor(guideObject, Color.ghostWhite);
        }
        
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

    public GameObject PlaceGuideObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 3;

            return Instantiate(Raft.Instance.simpleRaftTile, position, transform.rotation);
        }

        return null;
    }


    bool CanPlace()
    {
        if(raftTiles.ContainsKey(BuildGuideManager.Instance.gridPosition))
        {
            BuildGuideManager.Instance.ChangeColor(guideObject, Color.red);
            return false;
        }

        return true;
    }
}

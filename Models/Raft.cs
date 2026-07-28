using UnityEngine;

public class Raft : MonoBehaviour
{
    [Header ("Raft Tiles")]
    public GameObject simpleRaftTile;

    [Space]
    [Header ("Tile Size")]
    public float simpleTileSize = 41.5f;


    public static Raft Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
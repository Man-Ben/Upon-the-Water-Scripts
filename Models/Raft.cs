using UnityEngine;

public class Raft : MonoBehaviour
{
    [Header ("Raft Tiles")]
    public GameObject simpleRaftTile;

    [Space]
    [Header ("Tile Size")]
    public int simpleTileSize = 41;


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
using System.Collections.Generic;
using UnityEngine;

public class BuildingCategories : MonoBehaviour
{
    [Header ("Raft Tiles")]
    public List<GameObject> raftTiles;
    
    [Space]
    [Header ("Purifiers")]
    public List<GameObject> purifiers;

    [SerializeField] public Dictionary<Category, List<GameObject>> buildings;

    public static BuildingCategories Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        InitializeBuildingCategories();
    }

    public void InitializeBuildingCategories()
    {
        buildings.Add(Category.RaftTile, raftTiles);
        buildings.Add(Category.Water, purifiers);
    }

}
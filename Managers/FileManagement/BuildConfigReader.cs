using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildConfigReader : MonoBehaviour
{
    public static BuildConfigReader Instance {get; private set;}

    public BuildingStats buildingStats {get; set;}

    [SerializeField] public Dictionary<Category, List<BuildingStats>> buildingCatalog;
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SortDataByCategory();
    }

    public BuildingStats ReadBuildingStats(string buildingID)
    {
        TextAsset configPath = Resources.Load<TextAsset>($"Buildings/{buildingID}");

        buildingStats = JsonUtility.FromJson<BuildingStats>(configPath.text);

        return buildingStats;
    }

    public void SortDataByCategory()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>($"Buildings");

        buildingCatalog = new Dictionary<Category, List<BuildingStats>>();

        foreach(Category category in Enum.GetValues(typeof(Category)))
            buildingCatalog.Add(category, new List<BuildingStats>());
        
        foreach(TextAsset textAsset in textAssets)
        {
            buildingStats = JsonUtility.FromJson<BuildingStats>(textAsset.text);

            buildingCatalog[buildingStats.category].Add(buildingStats);
        }
            
    }

}

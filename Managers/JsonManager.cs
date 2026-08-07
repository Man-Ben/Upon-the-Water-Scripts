using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance {get; private set;}

    public BuildingStats buildingStats {get; set;}

    [SerializeField] public Dictionary<Category, List<string>> catalog; //Later replace this with icon names

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public BuildingStats ReadBuildingStats(string buildingID)
    {
        TextAsset configPath = Resources.Load<TextAsset>($"Buildings/{buildingID}");

        buildingStats = JsonUtility.FromJson<BuildingStats>(configPath.text);

        return buildingStats;
    }

    public void SortNameByCategory()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>($"Buildings");

        catalog = new Dictionary<Category, List<string>>();

        foreach(Category category in Enum.GetValues(typeof(Category)))
            catalog.Add(category, new List<string>());

        foreach(TextAsset textAsset in textAssets)
        {
            buildingStats = JsonUtility.FromJson<BuildingStats>(textAsset.text);
            Debug.Log($"{buildingStats.category} - {buildingStats.buildingName}");
            catalog[buildingStats.category].Add(buildingStats.buildingName);
        }
            
    }

}

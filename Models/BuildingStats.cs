using System;

[Serializable]
public class BuildingStats
{
    public string buildingName;
    public Category category;
    public Dimensions dimensions;
}

[Serializable]
public struct Dimensions
{
    public int length;
    public int width;
}

[Serializable]
public enum Category
{
    RaftTile,
    Water,
    Food,
    Expeditions,
    Defense
}
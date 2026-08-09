using System;
using UnityEngine;

[Serializable]
public class BuildingStats
{
    public string buildingName;
    public Category category;
    public Vector2Int footprint;
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
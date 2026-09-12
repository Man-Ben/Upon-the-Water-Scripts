using System;
using UnityEngine;

[Serializable]
public class BuildingStats
{
    public string buildingName;

    public int buildingLayer;
    public int capacity;

    public Category category;
    public Vector2Int footprint;
    public Cost cost;

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

[Serializable]
public struct Cost
{
    int woodCost;
    int leafCost;
    int plasticCost;
    int ironCost;
    int canonCost;
}
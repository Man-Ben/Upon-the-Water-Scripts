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
    public int woodCost;
    public int leafCost;
    public int plasticCost;
    public int stoneCost;
    public int ironCost;
    public int cannonCost;

    public int this[int index]
    {
        get => index switch
        {
            0 => woodCost,
            1 => leafCost,
            2 => plasticCost,
            3 => stoneCost,
            4 => ironCost,
            5 => cannonCost,
            _ => throw new IndexOutOfRangeException()
        };
    }
}
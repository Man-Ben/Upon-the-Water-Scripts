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
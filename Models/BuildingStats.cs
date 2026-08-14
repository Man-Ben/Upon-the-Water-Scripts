using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BuildingStats
{
    public int buildingLayer;
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
using System;
using UnityEngine;

public class BuildingCategories : MonoBehaviour
{
    public Raft raft = new Raft();
    public Water water = new Water();

    public static BuildingCategories Instance {get; private set;}

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
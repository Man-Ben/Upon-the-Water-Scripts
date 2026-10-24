using System;
using UnityEngine;

public class BuildGuideManager : MonoBehaviour
{
    public static BuildGuideManager Instance {get; private set;}

    public Vector3Int gridPosition;
    
    BuildConfigReader buildConfig;

    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        buildConfig = BuildConfigReader.Instance;
    }

    void InitializeSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    Vector3 HitPointPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 position = new Vector3(0, 0, 0);

        if(Physics.Raycast(ray, out RaycastHit hit))
        { 
                position = hit.point;
        }

        return position;
    }

    public GameObject PlaceGuideObject(GameObject buildingType, Category category, int index)
    {
        Vector3 position = HitPointPosition();
        
        gridPosition = WorldToCell(position, category, index);
            
        return Instantiate(buildingType, gridPosition, transform.rotation);
    }


    public void ChangeColor(GameObject guideObject, Color inputColor)
    {
        Renderer[] renderers;
        Color color;

        renderers = guideObject.GetComponentsInChildren<Renderer>();

        color = inputColor;
        color.a = 0.8f;

        foreach(Renderer renderer in renderers)
            renderer.material.color = color;
        
    }

    public void FollowMouse(GameObject guideObject, Category category, int index)
    {
        Vector3 position = HitPointPosition();

        gridPosition = WorldToCell(position, category, index);

        guideObject.transform.position = CellToWorld(gridPosition, category, index) + Vector3.up;
    }

    Vector3Int WorldToCell(Vector3 mousePosition, Category category, int index)
    {
        int chunkSize = 4;
        int baseLayer = 2;

        int buildingLayer = buildConfig.buildingCatalog[category][index].buildingLayer;

        return new Vector3Int(Mathf.RoundToInt(mousePosition.x / chunkSize), baseLayer + buildingLayer, Mathf.RoundToInt(mousePosition.z / chunkSize));
    }

    Vector3Int CellToWorld(Vector3Int gridPosition, Category category, int index)
    {
        int chunkSize = 4;
        int baseLayer = 2;

        int buildingLayer = buildConfig.buildingCatalog[category][index].buildingLayer;

        return new Vector3Int(gridPosition.x * chunkSize, baseLayer + buildingLayer, gridPosition.z * chunkSize);
    }

}

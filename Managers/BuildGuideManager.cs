using System;
using UnityEngine;

public class BuildGuideManager : MonoBehaviour
{
    public Vector3Int gridPosition;

    public static BuildGuideManager Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;        
        
    }

    public GameObject PlaceGuideObject(GameObject buildingType, Category category, int index)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 position = new Vector3(0, 0, 0);

        if(Physics.Raycast(ray, out RaycastHit hit))
        { 
                position = hit.point;
        }
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 position = new Vector3(0, 0, 0);

        if(Physics.Raycast(ray, out RaycastHit hit))
        { 
                position = hit.point;
        }

            gridPosition = WorldToCell(position, category, index);

            guideObject.transform.position = CellToWorld(gridPosition, category, index) + Vector3.up * 1f;
    }

    Vector3Int WorldToCell(Vector3 mousePosition, Category category, int index)
    {
        return new Vector3Int(Mathf.RoundToInt(mousePosition.x / 4), 2 + JsonManager.Instance.buildingCatalog[category][index].buildingLayer, Mathf.RoundToInt(mousePosition.z / 4));
    }

    Vector3Int CellToWorld(Vector3Int gridPosition, Category category, int index)
    {
        return new Vector3Int(Mathf.RoundToInt(gridPosition.x * 4), 2 + JsonManager.Instance.buildingCatalog[category][index].buildingLayer, Mathf.RoundToInt(gridPosition.z * 4));
    }

}

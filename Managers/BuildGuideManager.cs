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

    public GameObject PlaceGuideObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 3;

            return Instantiate(BuildingCategories.Instance.raft.simpleRaftTile, position, transform.rotation);
        }

        return null;
    }


    public void ChangeColor(GameObject guideObject, Color inputColor)
    {
        Renderer renderer;
        Color color;

        renderer = guideObject.GetComponent<Renderer>();

        color = inputColor;
        color.a = 0.8f;

        renderer.material.color = color;
        
    }

    public void FollowMouse(GameObject guideObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 3;

            gridPosition = WorldToCell(position);

            guideObject.transform.position = CellToWorld(gridPosition) + Vector3.up * 1;
        }
    }

    Vector3Int WorldToCell(Vector3 mousePosition)
    {
        return new Vector3Int(Mathf.RoundToInt(mousePosition.x / 41f), 3, Mathf.RoundToInt(mousePosition.z / 41));
    }

    Vector3Int CellToWorld(Vector3Int gridPosition)
    {
        return new Vector3Int(Mathf.RoundToInt(gridPosition.x * 41f), 3, Mathf.RoundToInt(gridPosition.z * 41));
    }

}

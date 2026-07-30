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

    public void ChangeColor(GameObject guideObject, Color inputColor)
    {
        Renderer renderer;
        Color color;

        renderer = guideObject.GetComponent<Renderer>();

        color = renderer.material.color;
        color.a = 0.3f;

        renderer.material.color = inputColor;
    }

    public void FollowMouse(GameObject guideObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 3;

            guideObject.transform.position = position;

            gridPosition = new Vector3Int(Mathf.RoundToInt(guideObject.transform.position.x / 41f), 3, Mathf.RoundToInt(guideObject.transform.position.z / 41));
        }
    }

}

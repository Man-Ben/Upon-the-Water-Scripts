using UnityEngine;

public class BuildGuideController : MonoBehaviour
{
    Renderer renderer;
    Color color;
    Material material;

    public static BuildGuideController Instance {get; private set;}
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;        
        
    }

    void ChangeColor()
    {
        renderer = GetComponent<Renderer>();
        material = GetComponent<Material>();

        color = renderer.material.color;

        color.a = 0.5f;
        material.color = Color.ghostWhite;
    }

    public Vector3 FollowMouse()
    {
        ChangeColor();

        float mousePositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mousePositionZ = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

        return new Vector3(mousePositionX, 3, mousePositionZ);
    }
}

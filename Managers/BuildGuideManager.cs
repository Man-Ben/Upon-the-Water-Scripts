using UnityEngine;

public class BuildGuideManager : MonoBehaviour
{
    

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

    public void ChangeColor(GameObject guideObject)
    {
        Renderer renderer;
        Color color;

        renderer = guideObject.GetComponent<Renderer>();

        color = renderer.material.color;
        color.a = 0.4f;

        renderer.material.color = Color.ghostWhite;
    }

    public void FollowMouse(GameObject guideObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;
            position.y = 5;

            guideObject.transform.position = position;
        }
    }
}

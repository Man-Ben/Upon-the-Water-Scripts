using UnityEngine;

public class Zoom : MonoBehaviour
{
    [Header ("Zoom Speed")]
    [Range (0, 100)]
    
    [SerializeField] float zoomSpeed;

    void LateUpdate()
    {
        Zooming();
        resetToBoundary();
    }

    void Zooming()
    {
        float zoomInput = Input.GetAxis("Zoom");
        
        transform.Translate( Vector3.forward * zoomInput * zoomSpeed);
    }

    void resetToBoundary()
    {
        float minHeight = 20f;
        float maxHeight = 70f;

        if(transform.position.y < minHeight)
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);

        if(transform.position.y > maxHeight)
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
    }
}

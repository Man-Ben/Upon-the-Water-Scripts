using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Player Speed")]
    [Range (0, 10)]

    [SerializeField] float playerSpeed;

    [Space]
    
    [Header ("Rotation Speed")]
    [Range (0, 10)]

    [SerializeField] float rotationSpeed;


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Rotation");

        transform.Translate(Vector3.forward * verticalInput * playerSpeed);
        transform.Translate(Vector3.right * horizontalInput * playerSpeed);
        transform.Rotate(Vector3.up * rotationSpeed * rotationInput);
    }
}

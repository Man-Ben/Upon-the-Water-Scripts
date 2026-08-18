using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header ("Speed")]
    [Range (0, 50)]
    [SerializeField] float shipSpeed;

    void Update()
    {
        gameObject.transform.Translate(Vector3.back * shipSpeed * Time.deltaTime);
    }
}

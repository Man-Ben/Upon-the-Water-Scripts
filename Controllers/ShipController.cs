using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header ("Speed")]
    [Range (0, 50)]
    [SerializeField] float shipSpeed;

    [Header("Layer Mask")]
    [SerializeField] LayerMask raftLayer;

    public enum ShipState
    {
        Moving,
        ReachedPlayerRaft
    }

    public ShipState shipState;

    void Update()
    {
        Move();
    }

    void LateUpdate()
    {
        PlayerRaftReached();
    }

    protected void Move()
    {
        if(shipState == ShipState.Moving)
            gameObject.transform.Translate(Vector3.back * shipSpeed * Time.deltaTime);
    }

    protected virtual void PlayerRaftReached()
    {
        if(Physics.Raycast(transform.position, Vector3.back, 10f, raftLayer) && shipState == ShipState.Moving)
        {
            EventUIManager.Instance.DisplayCategory(EventUIManager.EventCategory.SurivorArrival);
            shipState = ShipState.ReachedPlayerRaft;
        }
    }
}

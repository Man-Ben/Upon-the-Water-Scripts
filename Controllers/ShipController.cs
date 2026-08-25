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
        ReachedPlayerRaft,
        AcceptedOnBoard,
        RejectedToGetOnBoard,
    }

    public ShipState shipState;

    public static ShipController Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        Move();
        
        if(shipState != ShipState.RejectedToGetOnBoard && shipState == ShipState.Moving)
            PlayerRaftReached();
    }

    protected void Move()
    {
        if(shipState == ShipState.Moving || shipState == ShipState.RejectedToGetOnBoard)
            gameObject.transform.Translate(Vector3.back * shipSpeed * Time.deltaTime);

        if(transform.position.x <= -50f)
            Destroy(gameObject);
    }

    protected virtual void PlayerRaftReached()
    {
        if(Physics.Raycast(transform.position, Vector3.back, 10f, raftLayer))
        {
            EventUIManager.Instance.DisplayEvet(EventUIManager.EventCategory.SurivorArrival);
            shipState = ShipState.ReachedPlayerRaft;
        }
    }

    public virtual void Docking()
    {
            Destroy(gameObject);
    }

    public virtual void TurnShip()
    {
        transform.Rotate(new Vector3(0, 90, 0));
    }
}

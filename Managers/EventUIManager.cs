using UnityEngine;
using UnityEngine.UI;

public class EventUIManager : MonoBehaviour
{
    [Header ("Event UIs")]
    [SerializeField] GameObject newSurvivors;

    [Header ("Buttons")]
    [SerializeField] Button receiveSurvivors;
    [SerializeField] Button rejectSurvivors;
    [SerializeField] Button keepTheBoat;

    public enum EventCategory
    {
        neutral,
        SurivorArrival,
        NewIsland,
        PirateAttack
    }

    public EventCategory eventCategory;

    public static EventUIManager Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        AddListenerToUI();
    }

    void AddListenerToUI()
    {
        receiveSurvivors.onClick.AddListener(OnReceiveSurvivorsClicked);
        rejectSurvivors.onClick.AddListener(OnRejectSurvivorsClicked);
        keepTheBoat.onClick.AddListener(OnKeepShipClicked);
            
    }

    void OnReceiveSurvivorsClicked()
    {
        SurvivorManager.Instance.UpdateSurvivorData(1);
        ShipController.Instance.shipState = ShipController.ShipState.AcceptedOnBoard;

        ShipController.Instance.Docking();

        newSurvivors.SetActive(false);
    }

    void OnRejectSurvivorsClicked()
    {
        ShipController.Instance.shipState = ShipController.ShipState.RejectedToGetOnBoard;

        ShipController.Instance.TurnShip();

        newSurvivors.SetActive(false);
    }

    public void DisplayEvet(EventCategory eventCategory)
    {
        newSurvivors.SetActive(true);
    }

    public void OnKeepShipClicked()
    {
        if(!HarborManager.Instance.IsAHarborPlaced())
            Debug.Log("Harbor needed");

        if(!HarborManager.Instance.IsAvailableSlot())
            Debug.Log("Not enough space in harbor");
        else
        {
            HarborManager.Instance.UpdateSlots(1);
            SurvivorManager.Instance.UpdateSurvivorData(1);
            ShipController.Instance.shipState = ShipController.ShipState.AcceptedOnBoard;

            ShipController.Instance.Docking();

            newSurvivors.SetActive(false);
        }
    }
}

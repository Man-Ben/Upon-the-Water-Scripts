using TMPro;
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

    int newPeople;

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
        newPeople = Random.Range(1, 3);

        SurvivorManager.Instance.UpdateSurvivorData(newPeople);
        ShipController.Instance.shipState = ShipController.ShipState.AcceptedOnBoard;
        
        int nrOfUpdates = Random.Range(1, 3);
        InventoryManager.Instance.UpdateInventoryDisplay(nrOfUpdates);

        ShipController.Instance.Docking();

        newSurvivors.SetActive(false);
    }

    void OnRejectSurvivorsClicked()
    {
        ShipController.Instance.shipState = ShipController.ShipState.RejectedToGetOnBoard;

        ShipController.Instance.TurnShip();

        newSurvivors.SetActive(false);
    }

    public void DisplayEvent(EventCategory eventCategory)
    {
        if(!HarborManager.Instance.IsAHarborPlaced())
            keepTheBoat.transform.Find("Warning").GetComponent<TextMeshProUGUI>().text = "Harbor needed";
        else
            if(!HarborManager.Instance.IsAvailableSlot())
                keepTheBoat.transform.Find("Warning").GetComponent<TextMeshProUGUI>().text = "Not enough space in harbor";
            else
                keepTheBoat.transform.Find("Warning").GetComponent<TextMeshProUGUI>().text = " ";
                
        newSurvivors.SetActive(true);
    }

    public void OnKeepShipClicked()
    {
        if(!HarborManager.Instance.IsAHarborPlaced() || !HarborManager.Instance.IsAvailableSlot())
            return;

        HarborManager.Instance.UpdateSlots(1);

        newPeople = Random.Range(1, 3);
        SurvivorManager.Instance.UpdateSurvivorData(newPeople);
        
        int nrOfUpdates = Random.Range(1, 6);
        InventoryManager.Instance.UpdateInventoryDisplay(nrOfUpdates);
            
        ShipController.Instance.shipState = ShipController.ShipState.AcceptedOnBoard;
        ShipController.Instance.Docking();

        newSurvivors.SetActive(false);
    }
}

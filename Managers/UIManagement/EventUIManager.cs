using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUIManager : MonoBehaviour
{
    public static EventUIManager Instance {get; private set;}


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

    HarborManager harborManager;
    InventoryManager inventoryManager;
    ShipController shipController;
    SurvivorManager survivorManager;
    
    int newPeople;

    void Awake()
    {
        InitializeSingleton();
        InitializeInstances();
        AddListenerToUI();
    }

    void InitializeSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void InitializeInstances()
    {
        harborManager = HarborManager.Instance;
        inventoryManager = InventoryManager.Instance;
        shipController = ShipController.Instance;
        survivorManager = SurvivorManager.Instance;
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

        survivorManager.UpdateSurvivorData(newPeople);
        shipController.shipState = ShipController.ShipState.AcceptedOnBoard;
        
        int nrOfUpdates = Random.Range(1, 3);
        inventoryManager.UpdateInventoryDisplay(nrOfUpdates);

        shipController.Docking();

        newSurvivors.SetActive(false);
    }

    void OnRejectSurvivorsClicked()
    {
        shipController.shipState = ShipController.ShipState.RejectedToGetOnBoard;

        shipController.TurnShip();

        newSurvivors.SetActive(false);
    }

    public void DisplayEvent(EventCategory eventCategory)
    {
        if(eventCategory == EventCategory.SurivorArrival)
            ShipArrival();
    }

    void ShipArrival()
    {
        var warningText = keepTheBoat.transform.Find("Warning").GetComponent<TextMeshProUGUI>();
        if(!harborManager.IsAHarborPlaced())
            warningText.text = "Harbor needed";
        else
            if(!harborManager.IsAvailableSlot())
                warningText.text = "Not enough space in harbor";
            else
                warningText.text = " ";
                
        newSurvivors.SetActive(true);
    }

    public void OnKeepShipClicked()
    {
        if(!harborManager.IsAHarborPlaced() || !harborManager.IsAvailableSlot())
            return;

        harborManager.UpdateSlots(1);

        newPeople = Random.Range(1, 3);
        survivorManager.UpdateSurvivorData(newPeople);
        
        int nrOfUpdates = Random.Range(1, 6);
        inventoryManager.UpdateInventoryDisplay(nrOfUpdates);
            
        shipController.shipState = ShipController.ShipState.AcceptedOnBoard;
        shipController.Docking();

        newSurvivors.SetActive(false);
    }
}

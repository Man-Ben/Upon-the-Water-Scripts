using UnityEngine;
using UnityEngine.UI;

public class EventUIManager : MonoBehaviour
{
    [Header ("Event UIs")]
    [SerializeField] GameObject newSurvivors;

    [Header ("Buttons")]
    [SerializeField] Button receiveSurvivors;
    [SerializeField] Button rejectSurvivors;

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

    public void DisplayCategory(EventCategory eventCategory)
    {
        newSurvivors.SetActive(true);
    }
}

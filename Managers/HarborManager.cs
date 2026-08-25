using System.Linq;
using TMPro;
using UnityEngine;

public class HarborManager : MonoBehaviour
{
    [Header ("Text")]
    [SerializeField] TextMeshProUGUI availableSlots;
    [SerializeField] TextMeshProUGUI availableShips;

    [Space]
    [Header ("UI")]
    [SerializeField] GameObject freeShipIndicator;
    [SerializeField] GameObject freeSlotIndicator;

    int slots;
    int occupiedSlots;

    public static HarborManager Instance {get; private set;}

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    
        slots = JsonManager.Instance.buildingCatalog[Category.RaftTile][1].capacity;
        occupiedSlots = 0;

        UpdateSlots(0);
    }

    void Update()
    {
        if(IsAHarborPlaced())
        {
            freeShipIndicator.SetActive(true);
            freeSlotIndicator.SetActive(true);
        }
    }

    public bool IsAHarborPlaced()
    {
        return BuildManager.Instance.OccupiedCells.Values.Any(x => x.category == Category.RaftTile && x.index == 1);
    }

    public bool IsAvailableSlot()
    {
        if(slots - occupiedSlots > 0)
            return true;
        
        return false;
    }

    public void UpdateSlots(int modifier)
    {
        occupiedSlots += modifier;

        availableSlots.text = $"{slots - occupiedSlots} / {slots}";
    }
}

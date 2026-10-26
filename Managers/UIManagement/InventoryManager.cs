using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; set;}


    [Header ("UI")]
    [SerializeField] Transform contentTransform;
 
    [SerializeField] GameObject inventorySlot;

    [Space]

    [SerializeField] public Dictionary<string, int> inventory;
    
    public List<GameObject> inventorySlots;

    Dictionary<string, bool> isSlotUnlocked = new Dictionary<string, bool> 
    {
        {"Wood", true},
        {"Leaf", true}, 
        {"Plastic", false}, 
        {"Stone", false}, 
        {"Iron", false}, 
        {"Cannon", false}
    };

    void Awake()
    {
        InitializeSingleton(); 
    }

    void Start()
    {
        DisplayInventory();
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

    void DisplayInventory()
    {
        InitializeInventory();

        foreach(var slot in inventory)
        {
            GameObject tmp = Instantiate(inventorySlot, contentTransform);

            var NameText =  tmp.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var quantityText = tmp.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();

            NameText.text = slot.Key;
            quantityText.text = $"{slot.Value}";

            inventorySlots.Add(tmp);
        }
    }
    
    void InitializeInventory()
    {
        int i = 0;

        foreach(var slot in isSlotUnlocked)
        {            
            PlayerDataManager playerData = PlayerDataManager.Instance;

            if(slot.Value)
                inventory.Add(slot.Key, playerData.playersInventory.playerInventory[i]);

                i++;
        }
    }

    public void ChangeTextColor(int index, Color color)
    {
        int unlockedSlotNr = isSlotUnlocked.Count(x => x.Value);

        if(index >= unlockedSlotNr)
            return;

        var quantityText = inventorySlots[index].transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        quantityText.color = color;
    }

    public void UpdateInventoryDisplay(int nrOfUpdate)
    {
        int lastIndex = -1;
        int unlockedSlotNr = isSlotUnlocked.Count(x => x.Value);

        while(nrOfUpdate != 0)
        {
            PlayerDataManager playerData = PlayerDataManager.Instance;

            int randomIndex = Random.Range(0, unlockedSlotNr);
            int randomQuantity = Random.Range(5, 20);

            if(randomIndex == lastIndex)
                continue;

            playerData.playersInventory.playerInventory[randomIndex] += randomQuantity;

            var quantityText = inventorySlots[randomIndex].transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            quantityText.text = $"{playerData.playersInventory.playerInventory[randomIndex]}";

            nrOfUpdate--;

            lastIndex = randomIndex;
        }
    }

    public void WriteCost(Category category, int index, bool isClearingNeeded)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            var costText = inventorySlots[i].transform.Find("Cost").GetComponent<TextMeshProUGUI>();

            if(isClearingNeeded)
            {
                costText.text = $" ";
                continue;
            }

            int cost = BuildConfigReader.Instance.buildingCatalog[category][index].cost[i];

            if(cost != 0)               
                costText.text = $"{cost}";
            
        }
    }

    public void UpdateCostDisplay(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            PlayerDataManager playerData = PlayerDataManager.Instance;
            
            int cost = BuildConfigReader.Instance.buildingCatalog[category][index].cost[i];
            
            var quantityText = inventorySlots[i].transform.Find("Quantity").GetComponent<TextMeshProUGUI>();

            playerData.playersInventory.playerInventory[i] -= cost;

            quantityText.text = $"{playerData.playersInventory.playerInventory[i]}";
        }
    }

}

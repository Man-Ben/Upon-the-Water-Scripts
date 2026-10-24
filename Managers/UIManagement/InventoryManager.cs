using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; set;}


    [Header ("UI")]
    [SerializeField] Transform contentTransform;
 
    [SerializeField] GameObject inventorySlot;

    [Space]

    [SerializeField] public Dictionary<string, int> inventory;
    
    public List<GameObject> inventorySlots;

    string[] slotNames = {"Wood", "Leaf", "Plastic", "Stone", "Iron", "Cannon"};

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
        for(int i = 0; i < 6; i++)
        {            
            PlayerDataManager playerData = PlayerDataManager.Instance;

            inventory.Add(slotNames[i], playerData.playersInventory.playerInventory[i]);
        }
    }

    public void ChangeTextColor(int index, Color color)
    {
        var quantityText = inventorySlots[index].transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        quantityText.color = color;
    }

    public void UpdateInventoryDisplay(int nrOfUpdate)
    {
        while(nrOfUpdate != 0)
        {
            PlayerDataManager playerData = PlayerDataManager.Instance;

            int randomIndex = Random.Range(0, 2);
            int randomQuantity = Random.Range(5, 20);

            playerData.playersInventory.playerInventory[randomIndex] += randomQuantity;

            var quantityText = inventorySlots[randomIndex].transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
            quantityText.text = $"{playerData.playersInventory.playerInventory[randomIndex]}";

            nrOfUpdate--;
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

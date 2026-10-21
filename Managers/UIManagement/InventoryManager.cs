using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header ("UI")]
    [SerializeField] Transform contentTransform;
 
    [SerializeField] GameObject inventorySlot;

    [Space]

    [SerializeField] public Dictionary<string, int> inventory;
    
    public List<GameObject> inventorySlots;
    
    string[] slotNames = {"Wood", "Leaf", "Plastic", "Stone", "Iron", "Cannon"};

    public static InventoryManager Instance {get; set;}
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;     
    }

    void Start()
    {
        DisplayInventory();
    }

    void DisplayInventory()
    {
        InitializeInventory();

        foreach(var slot in inventory)
        {
            GameObject tmp = Instantiate(inventorySlot, contentTransform);

            tmp.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = slot.Key;
            tmp.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = $"{slot.Value}";

            inventorySlots.Add(tmp);
        }
    }
    
    void InitializeInventory()
    {
        for(int i = 0; i < 6; i++)
        {
            inventory.Add(slotNames[i], PlayerDataManager.Instance.playersInventory.playerInventory[i]);
        }
    }

    public void ChangeTextColor(int index, Color color)
    {
        inventorySlots[index].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().color = color;
    }

    public void UpdateInventoryDisplay(int nrOfUpdate)
    {
        while(nrOfUpdate != 0)
        {
            int randomIndex = Random.Range(0, 2);
            int randomQuantity = Random.Range(5, 20);

            PlayerDataManager.Instance.playersInventory.playerInventory[randomIndex] += randomQuantity;

            inventorySlots[randomIndex].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = $"{PlayerDataManager.Instance.playersInventory.playerInventory[randomIndex]}";

            nrOfUpdate--;
        }
    }

    public void WriteCost(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(BuildConfigReader.Instance.buildingCatalog[category][index].cost[i] != 0)
                inventorySlots[i].transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = $"{BuildConfigReader.Instance.buildingCatalog[category][index].cost[i]}";
        }
    }

    public void UpdateCostDisplay(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            PlayerDataManager.Instance.playersInventory.playerInventory[i] -= BuildConfigReader.Instance.buildingCatalog[category][index].cost[i];

            inventorySlots[i].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = $"{PlayerDataManager.Instance.playersInventory.playerInventory[i]}";
        }
    }

    public void ClearCostDisplay(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(BuildConfigReader.Instance.buildingCatalog[category][index].cost[i] != 0)
                inventorySlots[i].transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = $" ";
        }
    }
}

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

    public int baseQuantity = 30;
    
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
        foreach(string name in slotNames)
        {
            inventory.Add(name, baseQuantity);
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

    public void UpdateDisplay(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            baseQuantity -= BuildConfigReader.Instance.buildingCatalog[category][index].cost[i];

            inventorySlots[i].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = $"{baseQuantity}";
        }
    }

    public void ClearDisplay(Category category, int index)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(BuildConfigReader.Instance.buildingCatalog[category][index].cost[i] != 0)
                inventorySlots[i].transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = $" ";
        }
    }
}

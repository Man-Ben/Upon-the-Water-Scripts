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

    int baseQuantity = 30;
    
    string[] slotNames = {"Wood", "Leaf", "Plastic", "Iron", "Cannon"};

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
        }
    }
    
    void InitializeInventory()
    {
        foreach(string name in slotNames)
        {
            inventory.Add(name, baseQuantity);
        }
    }

    public void UpdateDisplay()
    {
        
    }
}

using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance {get; private set;}
    
    public PlayersInventory playersInventory {get; set;}
    
    void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    public PlayerInventory Inventory()
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Player Data");

        return JsonUtility.FromJson<PlayerInventory>(textAsset.text);
    }
}

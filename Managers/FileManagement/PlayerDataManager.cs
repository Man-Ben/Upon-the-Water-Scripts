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

        Instance = this;
        
        playersInventory = Inventory();

    }

    public PlayersInventory Inventory()
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"PlayerData/Inventory");

        return JsonUtility.FromJson<PlayersInventory>(textAsset.text);
    }
}

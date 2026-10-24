using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance {get; private set;}
    
    public PlayersInventory playersInventory {get; set;}
    
    void Awake()
    {
        InitializeSingleton();
        
        playersInventory = Inventory();
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

    public PlayersInventory Inventory()
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"PlayerData/Inventory");

        return JsonUtility.FromJson<PlayersInventory>(textAsset.text);
    }
}

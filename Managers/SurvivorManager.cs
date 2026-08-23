using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SurvivorManager : MonoBehaviour
{
    [Header ("Icons")]
    [SerializeField] Image waterPercentageIcon;
    [SerializeField] Image foodPercentageIcon;

    [Space]
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI waterPercentage;
    [SerializeField] TextMeshProUGUI foodPercentage;
    [SerializeField] TextMeshProUGUI numberOfSurvivorsText;

    int numberOfSurvivors = 2;

    float totalWaterCapacity = 0;
    float totalFoodCapacity = 0;

    public static SurvivorManager Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        UpdateNumberOfSurvivors(0);
    }

    public void UpdateSurvivorData(int modifier)
    {
        UpdateNumberOfSurvivors(modifier);
        UpdateFoodPercentage(0);
        UpdateWaterPercentage(0);
    }

    public void UpdateFoodPercentage(int modifier)
    {   
        totalFoodCapacity += modifier;

        float percentage = totalFoodCapacity / numberOfSurvivors * 100f;


        foodPercentage.text = $"{percentage:F0}%";
        foodPercentageIcon.fillAmount = percentage / 100f;
        
    }
    
    public void UpdateWaterPercentage(int modifier)
    {
        totalWaterCapacity += modifier;

        float percentage = totalWaterCapacity / numberOfSurvivors * 100f;

        waterPercentage.text = $"{percentage:F0}%";
        waterPercentageIcon.fillAmount = percentage / 100f;
    }

    public void UpdateNumberOfSurvivors(int modifier)
    {
        numberOfSurvivors += modifier;
        numberOfSurvivorsText.text = $"{numberOfSurvivors}";
    }
}

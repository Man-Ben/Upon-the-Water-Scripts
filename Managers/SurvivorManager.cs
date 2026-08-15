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

    int numberOfSurvivors = 10;

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

        UpdateNumberOfSurvivors();
    }

    public void UpdateFoodPercentage(Category category, int index)
    {   
        totalFoodCapacity += JsonManager.Instance.buildingCatalog[category][index].capacity;

        float percentage = totalFoodCapacity / numberOfSurvivors;

        foodPercentageIcon.fillAmount += percentage;

        foodPercentage.text = $"{percentage * 100f}%";
    }
    
    public void UpdateWaterPercentage(Category category, int index)
    {
        totalWaterCapacity += JsonManager.Instance.buildingCatalog[category][index].capacity;

        float percentage = totalWaterCapacity / numberOfSurvivors;

        waterPercentageIcon.fillAmount += percentage;

        waterPercentage.text = $"{percentage * 100f}%";
    }

    public void UpdateNumberOfSurvivors()
    {
        numberOfSurvivorsText.text = $"{numberOfSurvivors}";
    }
}

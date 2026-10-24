using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class SurvivorManager : MonoBehaviour
{
    public static SurvivorManager Instance {get; private set;}

    
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

    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        UpdateNumberOfSurvivors(0);
        StartCoroutine(KillSurvivors());
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

    void Update()
    {
        CheckSurvivorData();
    } 

    public void CheckSurvivorData()
    {
        if(numberOfSurvivors == 0)
        {
            Debug.Log("Game Over!");
            return;
        }
    }

    IEnumerator KillSurvivors()
    {
        while(true)
        {
            yield return new WaitUntil(() => totalFoodCapacity < numberOfSurvivors || totalWaterCapacity < numberOfSurvivors); 

            int randomDelay = Random.Range(1, 2);

            yield return new WaitForSecondsRealtime(randomDelay * 60);
         
            if(!(totalFoodCapacity < numberOfSurvivors || totalWaterCapacity < numberOfSurvivors))
                continue;
            
            UpdateNumberOfSurvivors(-1);
        }
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

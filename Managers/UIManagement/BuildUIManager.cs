using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildUIManager : MonoBehaviour
{

    [Header ("Scroll View Elements")]
    [SerializeField] Transform contentTransform;
    [SerializeField] Button buildButtonPrefab;
    [SerializeField] GameObject scrollView;

    [Space]
    [Header("Categories")]
    [SerializeField] List<Button> buildCategoryButtons = new List<Button>();

    List<Button> prefabButtons = new();

    GameObject guideObject;

    Category catByIndex;

    BuildingCategories buildingCategories;
    BuildConfigReader buildConfig;
    BuildGuideManager buildGuideManager;
    BuildManager buildManager;
    InventoryManager inventoryManager;
    SurvivorManager survivorManager;

    int index;

    void Start()
    {
        InitializeInstances();
    }

    void Update()
    {
        BuildControls();
    }

    void InitializeInstances()
    {
        buildingCategories = BuildingCategories.Instance;
        buildConfig = BuildConfigReader.Instance;
        buildGuideManager = BuildGuideManager.Instance;
        buildManager = BuildManager.Instance;
        inventoryManager = InventoryManager.Instance;
        survivorManager = SurvivorManager.Instance;
    }

    void OnPrefabButtonClicked(Button button)
    {
        
        index = prefabButtons.IndexOf(button);

        GameObject buildingType = buildingCategories.buildings[catByIndex][index];
        guideObject = buildGuideManager.PlaceGuideObject(buildingType, catByIndex, index);

        inventoryManager.WriteCost(catByIndex, index, false);

        foreach(Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        prefabButtons.Clear();

        scrollView.gameObject.SetActive(false);   
        
        foreach(var category in buildCategoryButtons)
        {
            category.gameObject.SetActive(false);
        }
    }

    void BuildControls()
    {
        if(guideObject != null)
        {
            buildGuideManager.FollowMouse(guideObject, catByIndex, index);
        
            if(buildManager.CanPlace(guideObject, catByIndex, index))
            {
                if(Input.GetMouseButtonDown(0))
                {
                    inventoryManager.UpdateCostDisplay(catByIndex, index);

                    GameObject objectToPlace = buildingCategories.buildings[catByIndex][index];

                    buildManager.PlaceBuilding(guideObject, objectToPlace, catByIndex, index);

                    int capacity = buildConfig.buildingCatalog[catByIndex][index].capacity;

                    if(catByIndex == Category.Water)
                        survivorManager.UpdateWaterPercentage(capacity);
                    else
                        if(catByIndex == Category.Food)
                            survivorManager.UpdateFoodPercentage(capacity);
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
                for(int i = 0; i < 6; i++)
                    inventoryManager.ChangeTextColor(i, Color.black);
 
                inventoryManager.WriteCost(catByIndex, index, true);
                Destroy(guideObject);
            }
        }  
        else
            ShowBuildMenu();
            
    }

    void ShowBuildMenu()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            for(int i = 0; i < buildCategoryButtons.Count; i++)
            {
                int index = i;

                buildCategoryButtons[i].gameObject.SetActive(true);

                buildCategoryButtons[i].onClick.RemoveAllListeners();
                buildCategoryButtons[i].onClick.AddListener(() => OnCategoryButtonClicked(index));
            }
        }
    }

    void OnCategoryButtonClicked(int index)
    {
        
        catByIndex = GetCategory(index);

        foreach(BuildingStats nameByCategory in buildConfig.buildingCatalog[catByIndex])
        {
            if(!nameByCategory.isUnlocked)
                continue;

            var tmp = Instantiate(buildButtonPrefab, contentTransform);

            tmp.GetComponentInChildren<TMP_Text>().text = nameByCategory.buildingName;

            tmp.onClick.AddListener(() => OnPrefabButtonClicked(tmp));
            
            prefabButtons.Add(tmp);
        }
    }

    Category GetCategory(int index)
    {
        return (Category)index;
    }
}

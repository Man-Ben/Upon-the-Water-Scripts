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

    GameObject guideObject;

    Category catByIndex;
    
    int index;

    List<Button> prefabButtons = new();

    void Update()
    {
        BuildControls();
    }

    void OnPrefabButtonClicked(Button button)
    {
        
        index = prefabButtons.IndexOf(button);

        guideObject = BuildGuideManager.Instance.PlaceGuideObject(BuildingCategories.Instance.buildings[catByIndex][index]);

        guideObject.GetComponent<Collider>().enabled = false;

        foreach(Transform child in contentTransform)
        {
            Destroy(child.gameObject);
            prefabButtons.Clear();
        }

        scrollView.gameObject.SetActive(false);   
        
        foreach(var category in buildCategoryButtons)
                category.gameObject.SetActive(false);
    }

    void BuildControls()
    {
        if(guideObject != null)
        {
            BuildGuideManager.Instance.FollowMouse(guideObject);
        
            if(BuildManager.Instance.CanPlace(guideObject, catByIndex, index))
            {
                

                if(Input.GetMouseButtonDown(0))
                {
                    BuildManager.Instance.PlaceBuilding(guideObject, BuildingCategories.Instance.buildings[catByIndex][index]);
                }
            }

            if(Input.GetMouseButtonDown(1))
            {
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
                buildCategoryButtons[i].onClick.AddListener(() => OnCategoryButtonClicked(index));
            }
        }
    }

    void OnCategoryButtonClicked(int index)
    {
        
        catByIndex = GetCategory(index);

        foreach(BuildingStats nameByCategory in JsonManager.Instance.buildingCatalog[catByIndex])
        {
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

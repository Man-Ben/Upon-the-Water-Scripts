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

    void Start()
    {
        JsonManager.Instance.SortNameByCategory();
    }

    void Update()
    {
        BuildControls();
    }

    void OnPrefabButtonClicked()
    {
        guideObject = BuildGuideManager.Instance.PlaceGuideObject();
        guideObject.GetComponent<Collider>().enabled = false;

        foreach(Transform child in contentTransform)
            Destroy(child.gameObject);

        scrollView.gameObject.SetActive(false);   
        
        foreach(var category in buildCategoryButtons)
                category.gameObject.SetActive(false);
    }

    void BuildControls()
    {
        if(guideObject != null)
        {
            BuildGuideManager.Instance.FollowMouse(guideObject);
        
            if(BuildManager.Instance.CanPlace(guideObject))
            {
                BuildGuideManager.Instance.ChangeColor(guideObject, Color.green);

                if(Input.GetMouseButtonDown(0))
                {
                    BuildManager.Instance.PlaceBuilding(guideObject);
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
        
        Category catByIndex = GetCategory(index);

        foreach(string category in JsonManager.Instance.catalog[catByIndex])
        {
            var tmp = Instantiate(buildButtonPrefab, contentTransform);

            tmp.GetComponentInChildren<TMP_Text>().text = category;

            tmp.onClick.AddListener(OnPrefabButtonClicked);
        }
    }

    Category GetCategory(int index)
    {
        return (Category)index;
    }
}

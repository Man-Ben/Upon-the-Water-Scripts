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
    [SerializeField] List<Button> buildCategories = new List<Button>();


    string[] categoryNames = {"Raft Tile", "Water Purifier", "Food", "Expedition"};

    GameObject guideObject;

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
        
        foreach(var category in buildCategories)
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

            for(int i = 0; i < buildCategories.Count; i++)
            {
                buildCategories[i].gameObject.SetActive(true);
                
            }

            DisplayBuildings(1);
        }
    }

    public void DisplayBuildings(int index)
    {
        var tmp = Instantiate(buildButtonPrefab, contentTransform);

        tmp.GetComponentInChildren<TMP_Text>().text = categoryNames[index];
       
        tmp.onClick.AddListener(OnPrefabButtonClicked);
    }
}

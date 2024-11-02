using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchPreview : MonoBehaviour
{
    [Header("Settings")]
    public float invokeDelay;
    public int distance = 200;


    [Header("Variables")] 
    public int curPage;
    public int curIndex;
    public int curDistance;

    [Header("Other")]
    public SearchManager searchManager;
    public TMP_Text titleText;
    public TMP_Text artistText;
    public TMP_Text yearText;
    public TMP_Text labelText;
    public Image imgTest;
    public RectTransform contentTransform;
    

    void Awake() {
        searchManager = GameObject.Find("Canvas").GetComponent<SearchManager>();        
    }

    private void Start()
    {
        //Initialize();
        Invoke(nameof(Initialize), invokeDelay);
    }

    void Initialize()
    {
        curDistance = curIndex * distance;
        this.transform.localPosition = new Vector3(0, 750 - curDistance, 0);
        
        if (searchManager.masterResult.results.Length != 0 
            && searchManager.masterResult.results[curIndex].title != null 
            && searchManager.masterResult.results[curIndex].title.Contains(" - ")) {
            
            string[] splitArray = searchManager.masterResult.results[curIndex].title.Split(" - ");
            string title = splitArray[1];
            string artist = splitArray[0];

            titleText.text = title;
            artistText.text = artist; 
        } else {
            titleText.text = "---"; 
        }
        Texture2D curImg = searchManager.imgD[curIndex * 2];
        imgTest.sprite = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));

        if(searchManager.masterResult.results[curIndex].year != null) {
            yearText.text = searchManager.masterResult.results[curIndex].year;
        } else {
            yearText.text = "----";
        }

        if(searchManager.masterResult.results[curIndex].label != null) {
            labelText.text = searchManager.masterResult.results[curIndex].label[0];
        } else {
            labelText.text = "---";
        }

        StartCoroutine(nameof(RefreshLayout));
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
    }

    public void SelectMaster()
    {
        searchManager.GetMasterID(curIndex, curPage);
    }
}

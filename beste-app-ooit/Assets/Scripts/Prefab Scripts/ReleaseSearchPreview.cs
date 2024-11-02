using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReleaseSearchPreview : MonoBehaviour
{
    [Header("Settings")]
    public float invokeDelay;
    public int distance = 200;
    

    [Header("Variables")]
    public int curIndex;
    public int curPage;
    public int curDistance;
    private Sprite coverImage;

    [Header("Objects")]
    public SearchManager searchManager;
    public TMP_Text titleText;
    public TMP_Text yearText;
    public TMP_Text countryText;
    public TMP_Text editionText;
    public Image imgTest;
    public RectTransform contentTransform;

    void Awake() {
        searchManager = GameObject.Find("Canvas").GetComponent<SearchManager>();        
    }

    void Start() {
        curDistance = curIndex * distance;
        this.transform.localPosition = new Vector3(0, 750 - curDistance, 0);

        
        if (searchManager.releaseResult.versions.Length != 0
            && searchManager.releaseResult.versions[curIndex].title != null) {
            
            titleText.text = searchManager.releaseResult.versions[curIndex].title;
            Texture2D curImg = searchManager.imgD[curIndex * 2];
            coverImage = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));
            imgTest.sprite = coverImage;
            
        } else { 
            titleText.text = "---";
        }

        if(searchManager.releaseResult.versions[curIndex].released != null) {
            yearText.text = searchManager.releaseResult.versions[curIndex].released;
        }
        else
        {
            yearText.text = "----";
        }

        if(searchManager.releaseResult.versions[curIndex].country != null) { 
            countryText.text = searchManager.releaseResult.versions[curIndex].country;
        } else { 
            countryText.text = "---";
        }
        if(searchManager.releaseResult.versions[curIndex].format != null) {
            string[] splitArray = searchManager.releaseResult.versions[curIndex].format.Split(", ");
            if(splitArray.Length >= 3) {
                    editionText.text = splitArray[2]; 
            } else {
                    editionText.text = "Standard";
            }
        }
        StartCoroutine("VerticalLayoutRefresh");
    }   

    private IEnumerator VerticalLayoutRefresh() {
        yield return new WaitForEndOfFrame(); 
        // Force the layout updates in the correct order
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform); // Rebuild parent (Release Preview content)
    }

    public void SelectRelease() {
        searchManager.GetReleaseID(curIndex, curPage, coverImage);
    }
}

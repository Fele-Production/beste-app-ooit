using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReleaseSearchPreview : MonoBehaviour
{
    public SearchManager searchManager;
    public TMP_Text titleText;
    public TMP_Text yearText;
    public TMP_Text countryText;
    public TMP_Text editionText;
    public Image imgTest;
    public int curPosition;
    public int distance;
    public int curDistance;
    public int pageBuffer;

    void Awake() {
        searchManager = GameObject.Find("Canvas").GetComponent<SearchManager>();        
    }

    void Start() {
        curDistance = curPosition * distance;
        this.transform.localPosition = new Vector3(0, 750 - curDistance, 0);
        pageBuffer = (searchManager.curPage-1) * searchManager.resultsPerPage;

        
        if (searchManager.releaseResult.versions.Length != 0) {
            if(searchManager.releaseResult.versions[curPosition + pageBuffer].title != null) {
                    titleText.text = searchManager.releaseResult.versions[curPosition + pageBuffer].title;
            Texture2D curImg = searchManager.imgD[(curPosition + pageBuffer) * 2];
            imgTest.sprite = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));
            
            } else {
                titleText.text = "---";
            }

            if(searchManager.releaseResult.versions[curPosition + pageBuffer].released != null) {
                yearText.text = searchManager.releaseResult.versions[curPosition + pageBuffer].released;
            } else {
                yearText.text = "----";
            } 

            if(searchManager.releaseResult.versions[curPosition + pageBuffer].country != null) {
                countryText.text = searchManager.releaseResult.versions[curPosition + pageBuffer].country;
            } else {
                countryText.text = "---";
            } 

            if(searchManager.releaseResult.versions[curPosition + pageBuffer].format != null) {
                string[] splitArray = searchManager.releaseResult.versions[curPosition + pageBuffer].format.Split(", ");
                if(splitArray.Length >= 3) {
                    editionText.text = splitArray[2];
                } else {
                    editionText.text = "Standard";
                }
            }
        }   
    }

    public void SelectRelease() {
        searchManager.GetReleaseID(curPosition);
    }
}

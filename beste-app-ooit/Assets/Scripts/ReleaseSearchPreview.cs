using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReleaseSearchPreview : MonoBehaviour
{
 public UIManager uiManager;
    public TMP_Text titleText;
    public TMP_Text yearText;
    public TMP_Text countryText;
    public TMP_Text editionText;
    public Image imgTest;
    public int curPosition;
    public int distance = 200;
    public int curDistance;
    public int pageBuffer;

    void Awake() {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();        
    }

    void Start() {
        curDistance = curPosition * distance;
        this.transform.localPosition = new Vector3(0, 750 - curDistance, 0);
        pageBuffer = (uiManager.curPage-1) * uiManager.resultsPerPage;

        
        if (uiManager.releaseResult.versions.Length != 0) {
            if(uiManager.releaseResult.versions[curPosition + pageBuffer].title != null) {
                    titleText.text = uiManager.releaseResult.versions[curPosition + pageBuffer].title;
            Texture2D curImg = uiManager.imgD[(curPosition + pageBuffer) * 2];
            imgTest.sprite = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));
            
            } else {
                titleText.text = "---";
            }

            if(uiManager.releaseResult.versions[curPosition + pageBuffer].released != null) {
                yearText.text = uiManager.releaseResult.versions[curPosition + pageBuffer].released;
            } else {
                yearText.text = "----";
            } 

            if(uiManager.releaseResult.versions[curPosition + pageBuffer].country != null) {
                countryText.text = uiManager.releaseResult.versions[curPosition + pageBuffer].country;
            } else {
                countryText.text = "---";
            } 

            if(uiManager.releaseResult.versions[curPosition + pageBuffer].format != null) {
                string[] splitArray = uiManager.releaseResult.versions[curPosition + pageBuffer].format.Split(", ");
                if(splitArray.Length >= 3) {
                    editionText.text = splitArray[2];
                } else {
                    editionText.text = "Standard";
                }
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchPreview : MonoBehaviour
{
    public SearchManager searchManager;
    public TMP_Text titleText;
    public TMP_Text artistText;
    public TMP_Text yearText;
    public TMP_Text labelText;
    public Image imgTest;
    public int curPosition;
    public int distance = 200;
    public int curDistance;
    public int pageBuffer;

    void Awake() {
        searchManager = GameObject.Find("Canvas").GetComponent<SearchManager>();        
    }

    // Start is called before the first frame update
    void Start() {
        curDistance = curPosition * distance;
        this.transform.localPosition = new Vector3(0, 750 - curDistance, 0);
        pageBuffer = (searchManager.curPage-1) * searchManager.resultsPerPage;
        GameObject textsParent = transform.Find("Texts").gameObject;
        textsParent.GetComponent<VerticalLayoutGroup>().spacing = 5;
        
        if (searchManager.masterResult.results.Length != 0) {
            if(searchManager.masterResult.results[curPosition + pageBuffer].title != null) {
                if(searchManager.masterResult.results[curPosition + pageBuffer].title.Contains(" - ")) {
                    string[] splitArray = searchManager.masterResult.results[curPosition + pageBuffer].title.Split(" - ");
                    string title = splitArray[1];
                    string artist = splitArray[0];

                    titleText.text = title;
                    artistText.text = artist;
                }
            Texture2D curImg = searchManager.imgD[(curPosition + pageBuffer) * 2];
            imgTest.sprite = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));
            
            } else {
                titleText.text = "---";
            }

            if(searchManager.masterResult.results[curPosition + pageBuffer].year != null) {
                yearText.text = searchManager.masterResult.results[curPosition + pageBuffer].year;
            } else {
                yearText.text = "----";
            }

            if(searchManager.masterResult.results[curPosition  + pageBuffer ].label != null) {
                labelText.text = searchManager.masterResult.results[curPosition + pageBuffer].label[0];
            } else {
                labelText.text = "---";
            }
        }     
    }

    public void SelectMaster() {
        searchManager.GetMasterID(curPosition);
    }

    
}

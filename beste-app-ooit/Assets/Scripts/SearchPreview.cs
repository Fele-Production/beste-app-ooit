using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchPreview : MonoBehaviour
{
    public UiManager uiManager;
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
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        curDistance = curPosition * distance;
        this.transform.localPosition = new Vector3(0, 1200 - curDistance, 0);
        pageBuffer = (uiManager.curPage-1) * uiManager.resultsPerPage;
        Debug.Log(pageBuffer);


        if(uiManager.jsonResult.results[curPosition + pageBuffer].title != null) {
            if(uiManager.jsonResult.results[curPosition + pageBuffer].title.Contains(" - ")) {
                string[] splitArray = uiManager.jsonResult.results[curPosition + pageBuffer].title.Split(" - ");
                string title = splitArray[1];
                string artist = splitArray[0];

                titleText.text = title;
                artistText.text = artist;
            }
        Texture2D curImg = uiManager.imgD[(curPosition + pageBuffer) * 2];
        imgTest.sprite = Sprite.Create(curImg,new Rect(0,0,curImg.width,curImg.height), new Vector2(0,0));
            
        } else {
            titleText.text = "---";
        }

        if(uiManager.jsonResult.results[curPosition + pageBuffer].year != null) {
            yearText.text = uiManager.jsonResult.results[curPosition + pageBuffer].year;
        } else {
            yearText.text = "----";
        }

        if(uiManager.jsonResult.results[curPosition  + pageBuffer ].label != null) {
            labelText.text = uiManager.jsonResult.results[curPosition + pageBuffer].label[0];
        } else {
            labelText.text = "---";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

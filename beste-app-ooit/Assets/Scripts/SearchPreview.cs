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

    void Awake() {
        
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>(); 

        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        curDistance = curPosition * distance;
        this.transform.localPosition = new Vector3(0, 1200 - curDistance, 0);

        if(uiManager.jsonResult.results[curPosition].title != null) {
            if(uiManager.jsonResult.results[curPosition].title.Contains(" - ")) {
                string[] splitArray = uiManager.jsonResult.results[curPosition].title.Split(" - ");
                string title = splitArray[1];
                string artist = splitArray[0];

                titleText.text = title;
                artistText.text = artist;
            }

        imgTest.sprite = Sprite.Create(uiManager.imgD,new Rect(0,0,uiManager.imgD.width,uiManager.imgD.height), new Vector2(0,0));
            
        } else {
            titleText.text = "---";
        }

        if(uiManager.jsonResult.results[curPosition].year != null) {
            yearText.text = uiManager.jsonResult.results[curPosition].year;
        } else {
            yearText.text = "----";
        }

        if(uiManager.jsonResult.results[curPosition].label != null) {
            labelText.text = uiManager.jsonResult.results[curPosition].label[0];
        } else {
            labelText.text = "---";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

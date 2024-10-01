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

    void Awake() {
        this.transform.localPosition = new Vector3(0, 1200, 0);
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>(); 
        if(uiManager.jsonResult.results[0].title != null) {
            if(uiManager.jsonResult.results[0].title.Contains(" - ")) {
                string[] splitArray = uiManager.jsonResult.results[0].title.Split(" - ");
                string title = splitArray[1];
                string artist = splitArray[0];

                titleText.text = title;
                artistText.text = artist;
            }

        imgTest.sprite = Sprite.Create(uiManager.imgD,new Rect(0,0,uiManager.imgD.width,uiManager.imgD.height), new Vector2(0,0));
            
        } else {
            titleText.text = "---";
        }

        if(uiManager.jsonResult.results[0].year != null) {
            yearText.text = uiManager.jsonResult.results[0].year;
        } else {
            yearText.text = "----";
        }

        if(uiManager.jsonResult.results[0].label != null) {
            labelText.text = uiManager.jsonResult.results[0].label[0];
        } else {
            labelText.text = "---";
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

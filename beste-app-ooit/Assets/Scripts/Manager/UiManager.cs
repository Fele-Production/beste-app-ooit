using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Discogs;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour {
    [Header("Search Settings")]
    public int resultsPerPage;
    public bool perfMode;

    [Header("Search Objects")]
    public SearchManager searchManager;
    public GameObject searchMenu;
    public GameObject confirmReleaseMenu;
    public GameObject saveReleaseMenu;
    public GameObject searchedMenu; 
    public TMP_InputField searchPrompt;
    public TMP_Text searchingText;
    public TMP_Text addSearchingText;
    public Button nextButton;
    public Button backButton;
    public Button moreButton;
    
    [Header ("Search Other")]
    public float searchingAnimDelay;

    [Header ("Theme Actors")]
    public Fantassimo discoScript;
    public Image backdropImage;
    private List<Texture2D> BackdropOptions = new();
    private int currentBackdrop;
    [SerializeField] public UserSettings userSettings = new();
    
    
    //Goes to the next results page
    public void NextPage() {
        searchManager.curPage++;
        backButton.interactable = true;
        
        //Check if there are more results to load, if not disable the next button
        switch (searchManager.curType)
        {
            case "master":
            {
                
                if(searchManager.masterResult.pagination.items > searchManager.curPage * resultsPerPage && searchManager.masterResult.results.Length == searchManager.curPage * resultsPerPage)
                {
                    searchManager.SearchMaster();
                    nextButton.interactable = true;
                } 
                else if(searchManager.masterResult.results.Length <= searchManager.curPage * resultsPerPage) {
                    nextButton.interactable = false;
                }

                break;
            }
            case "release":
            {   
                if(searchManager.releaseResult.pagination.items <= searchManager.curPage * resultsPerPage)
                {
                    searchManager.SearchRelease();
                    nextButton.interactable = true;
                }
                else if(searchManager.releaseResult.versions.Length <= searchManager.curPage * resultsPerPage) {
                    nextButton.interactable = false;
                }

                break;
            }
        }
        searchManager.FirstSearchResults(); 
    }
    
    //Goes to the previous results page
    public void PreviousPage() {
        searchManager.curPage--;
        nextButton.interactable = true;
        
        //Checks if on page 1, if so disable the back button
        if(searchManager.curPage == 1) {
            backButton.interactable = false;
        }   

        searchManager.FirstSearchResults();
    }
    
    //SearchingAnimation; Changes the text every few seconds, as if it's an animation
    public IEnumerator FirstSearchingAnimation() {
        searchingText.enabled = true;
        while (!searchManager.searched) {
            searchingText.text = "Searching. ";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "Searching..";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "Searching...";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
        }
        searchingText.enabled = false;
    }
    
    public IEnumerator AddSearchingAnimation() {
        addSearchingText.enabled = true;
        moreButton.enabled = false;
        while (!searchManager.searched) {
            searchingText.text = ".";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "..";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "...";
            if (searchManager.searched) {break;}
            yield return new WaitForSeconds(searchingAnimDelay);
        }
        searchingText.enabled = false;
        moreButton.enabled = true;
    }

    void Start() {
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/BLUE BACKDROP aka ORIGINAL pixil-frame-0 (12).png"));
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/BEIGE BACKDROP pixil-frame-0 (11).png"));
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/GREEN BACKDROP pixil-frame-0 (10).png"));
        
        searchedMenu = searchMenu.transform.Find("Searched Menu").gameObject;
    }

    void Update() {
        //UserSettings userSettings = Settings.Load(); we hebben nog niks om settings aan te passen dus
        discoScript.greenlit = userSettings.Theme.Fantassimo;

        if (currentBackdrop!=userSettings.Theme.Backdrop) {
            try {
                backdropImage.sprite = Sprite.Create(BackdropOptions[userSettings.Theme.Backdrop],new Rect(0,0,BackdropOptions[userSettings.Theme.Backdrop].width,BackdropOptions[userSettings.Theme.Backdrop].height), new Vector2(0,0));
                currentBackdrop = userSettings.Theme.Backdrop;
            }
            catch {
                //Yeah idfk just let the rollie do what it do
            }
        }
    }
}

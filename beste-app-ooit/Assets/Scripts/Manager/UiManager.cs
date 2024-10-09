using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Discogs;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour {
    [Header("Search Settings")]
    public int resultsPerPage;
    public int searchResultsPerPage;
    [SerializeField] public bool PerfMode;

    [Header("Search Objects")]
    public SearchManager searchManager;
    public GameObject searchMenu;
    public GameObject confirmReleaseMenu;
    public GameObject saveReleaseMenu;
    public GameObject searchedMenu; 
    public TMP_InputField searchPrompt;
    public TMP_Text searchingText;
    public GameObject searchMasterPreviewPrefab;
    public GameObject searchReleasePreviewPrefab;
    public Button nextButton;
    public Button backButton;
    public List<GameObject> curSearchPreviews;

    [Header ("Search Other")]
    public float searchingAnimDelay;

    [Header ("Theme Actors")]
    public Fantassimo discoScript;
    public Image backdropImage;
    [HideInInspector] List<Texture2D> BackdropOptions = new();
    [HideInInspector] int currentBackdrop = -1;
    [SerializeField] public UserSettings userSettings = new();

    public void NextPage() {
        searchManager.curPage++;
        backButton.interactable = true;

        if(searchManager.curType == "master") {
            if(searchManager.masterResult.results.Length <= searchManager.curPage * resultsPerPage) {
                nextButton.interactable = false;
            } 
        } else if(searchManager.curType == "release") {
            if(searchManager.releaseResult.versions.Length <= searchManager.curPage * resultsPerPage) {
                nextButton.interactable = false;
            }
        }
             

        RefreshSearch();  
    }

    public void PreviousPage() {
        searchManager.curPage--;
        nextButton.interactable = true;
        if(searchManager.curPage == 1) {
            backButton.interactable = false;
        }   

        RefreshSearch();
    }

    public static void RefreshLibrary() {
        GameManager.instance.RefreshLibrary();
    }


    public void RefreshSearch() {
        SendMessage("imstillalive");
        for(int i = 0; i < curSearchPreviews.Count; i++) {
            Destroy(curSearchPreviews[i]);
        }

        searchManager.searched = true;
        curSearchPreviews.Clear();
        StopCoroutine(SearchingAnimation());
        int resultsToLoad = resultsPerPage;

        if(searchManager.curType == "master") {
            if(resultsPerPage > (searchManager.masterResult.results.Length - ((searchManager.curPage-1) * resultsPerPage))) {
                resultsToLoad = searchManager.masterResult.results.Length - ((searchManager.curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                curSearchPreviews.Add(Instantiate(searchMasterPreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                curSearchPreviews[i].GetComponent<SearchPreview>().curPosition = i; 
                
            }
        } else if (searchManager.curType == "release") {
            if(resultsPerPage > (searchManager.releaseResult.versions.Length - ((searchManager.curPage-1) * resultsPerPage))) {
                resultsToLoad = searchManager.releaseResult.versions.Length - ((searchManager.curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                    curSearchPreviews.Add(Instantiate(searchReleasePreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                    curSearchPreviews[i].GetComponent<ReleaseSearchPreview>().curPosition = i; 
                }
            
        }
    }     

    public IEnumerator SearchingAnimation() {
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

    void Start() {
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/BLUE BACKDROP aka ORIGINAL pixil-frame-0 (12).png"));
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/BEIGE BACKDROP pixil-frame-0 (11).png"));
        BackdropOptions.Add(Get.ImageFromPath("Assets/Sprites/Lefonki Designs/Plaat fanaat players n backdrops with animated frames/Backdrops/GREEN BACKDROP pixil-frame-0 (10).png"));
        
        searchedMenu = searchMenu.transform.Find("Searched Menu").gameObject;
        nextButton = searchedMenu.transform.Find("Forward").GetComponent<Button>();
        backButton = searchedMenu.transform.Find("Back").GetComponent<Button>();
    }

    void Update() {
        //UserSettings userSettings = Settings.Load();
        discoScript.greenlit = userSettings.Theme.Fantassimo;
        if (currentBackdrop!=userSettings.Theme.Backdrop) {
            backdropImage.sprite = Sprite.Create(BackdropOptions[userSettings.Theme.Backdrop],new Rect(0,0,BackdropOptions[userSettings.Theme.Backdrop].width,BackdropOptions[userSettings.Theme.Backdrop].height), new Vector2(0,0));
            currentBackdrop = userSettings.Theme.Backdrop;
        }
    }
}

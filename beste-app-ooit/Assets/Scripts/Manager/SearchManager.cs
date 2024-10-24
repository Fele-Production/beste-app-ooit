using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Discogs;

public class SearchManager : MonoBehaviour
{
    public UIManager uiManager;

    [Header("Search Settings")]
    public int resultsPerPage;
    public int searchResultsPerPage;
    [SerializeField] public bool PerfMode;

    [Header ("Search Other")]
    public List<GameObject> curSearchPreviews;
    public List<Texture2D> imgD;
    public List<string> urls;
    public int curPage = 1;
    public string curType = "master";
    public float searchingAnimDelay;
    [HideInInspector] public int pageBuffer {get; private set;}
    public bool searched = false;
    [SerializeField] private int curMasterID;
    [SerializeField] private int curReleaseID;
    [SerializeField] private Sprite curCoverImg;

    [Header ("Search Results")]
    [SerializeField] public Discogs.Master masterResult = new ();
    [SerializeField] public Discogs.Release releaseResult = new ();
    [SerializeField] public Discogs.ReleaseInfo releaseInfo = new ();

   
    //Search Master based on the prompt given 
    public async void SearchMaster() {  
        searched = false;
        curType = "master";

        for(int i = 0; i < uiManager.curSearchPreviews.Count; i++) {
            Destroy(uiManager.curSearchPreviews[i]);
        }
        uiManager.curSearchPreviews.Clear();

        uiManager.StartCoroutine(uiManager.SearchingAnimation());
        masterResult = await Discogs.Get.Masters(uiManager.searchPrompt.text,1, searchResultsPerPage);

        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        curPage = 1;
        uiManager.backButton.interactable = false;
        if(masterResult.results.Length > resultsPerPage) { uiManager.nextButton.interactable = true; } else { uiManager.nextButton.interactable = false; }  

        //Refresh Image URL's
        urls.Clear();

        for(int i = 0; i < masterResult.results.Length; i++) {
            urls.Add(masterResult.results[i+pageBuffer].cover_image);
        }
        imgD = await Discogs.Get.ImageList(urls);

        uiManager.RefreshSearch();
    }

    //Search Releases based on MasterID given
    public async void SearchRelease() {
        searched = false;
        curType = "release";

        for(int i = 0; i < uiManager.curSearchPreviews.Count; i++) {
            Destroy(uiManager.curSearchPreviews[i]);
        }
        uiManager.curSearchPreviews.Clear();

        uiManager.StartCoroutine(uiManager.SearchingAnimation());
        releaseResult = await Discogs.Get.Releases(curMasterID, 1, searchResultsPerPage);
        
        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        curPage = 1;
        uiManager.backButton.interactable = false;
        if(releaseResult.versions.Length > resultsPerPage) { uiManager.nextButton.interactable = true; } else { uiManager.nextButton.interactable = false; }  
        
        //Refresh Image URL's
        urls.Clear();

        //if (/*Discogs.Settings.Load().Settings.PerformanceMode||PerfMode) { */ 
            for(int i = 0; i < releaseResult.versions.Length; i++) {
                urls.Add(releaseResult.versions[i+pageBuffer].thumb);
            }
        /*} else { //wip werkt nog niet hou PerfMode dus aan 
            for(int i = 0; i < releaseResult.versions.Length; i++) {
                urls.Add((await Get.ReleaseInfo(releaseResult.versions[i+pageBuffer].id)).images[0].uri);
            }
        }*/
        imgD = await Discogs.Get.ImageList(urls);

        uiManager.RefreshSearch();
    }


    //Get ReleaseInfo based on ReleaseID and saves it
    public async void SaveRelease() {
        
        UserLibrary oldLibrary = GameManager.instance.library;
        releaseInfo = await Get.ReleaseInfo(curReleaseID);
        await Library.Add(releaseInfo);
        UserLibrary curLibrary = Library.Load();

        //Checks if library has/hasn't changed (i.e, you have this release already)
        if(oldLibrary != curLibrary) {    
            GameManager.instance.AddLibrary(curCoverImg); 
        } else {
            Debug.LogWarning("Library hasn't changed"); 
        }
        
    }


    //Selects the Master ID based on the index given
    public void GetMasterID(int _index) {
        curMasterID = masterResult.results[_index + ((curPage-1)*resultsPerPage)].master_id;
        
        uiManager.confirmReleaseMenu.SetActive(true);
    }
    
    //Selects the Release ID based on the index given
    public void GetReleaseID(int _index, Sprite _sprite) {
        curReleaseID = releaseResult.versions[_index + ((curPage-1)*resultsPerPage)].id;
        curCoverImg = _sprite;


        uiManager.saveReleaseMenu.SetActive(true);
    }

    //Only enables the Next and back buttons if searched == true
    void Update() {
        if(searched) {
            uiManager.searchedMenu.SetActive(true);
            uiManager.searchingText.enabled = false;
        } else {
            uiManager.searchedMenu.SetActive(false); 
        }
    }
}

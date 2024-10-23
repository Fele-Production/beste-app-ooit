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

   

    public async void SearchMaster() {
        curType = "master";
        uiManager.StartCoroutine(uiManager.SearchingAnimation());
        masterResult = await Discogs.Get.Masters(uiManager.searchPrompt.text,1, searchResultsPerPage);
        
        curPage = 1;
        uiManager.backButton.interactable = false;
        if(masterResult.results.Length > resultsPerPage) { uiManager.nextButton.interactable = true; } else { uiManager.nextButton.interactable = false; }  
        
        urls.Clear();

        for(int i = 0; i < masterResult.results.Length; i++) {
            urls.Add(masterResult.results[i+pageBuffer].cover_image);
        }
        imgD = await Discogs.Get.ImageList(urls);

        uiManager.RefreshSearch();
    }

    public async void SearchRelease() {
        Debug.Log("Searching Releases...");
        uiManager.StartCoroutine(uiManager.SearchingAnimation());
        releaseResult = await Discogs.Get.Releases(curMasterID, 1, searchResultsPerPage);
        Debug.Log("Finished Searching for releases");
        curType = "release";
        
        curPage = 1;
        uiManager.backButton.interactable = false;
        if(releaseResult.versions.Length > resultsPerPage) { uiManager.nextButton.interactable = true; } else { uiManager.nextButton.interactable = false; }  
        
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

    public async void SaveRelease() {
        
        UserLibrary oldLibrary = GameManager.instance.library;
        releaseInfo = await Get.ReleaseInfo(curReleaseID);
        await Library.Add(releaseInfo);
        UserLibrary curLibrary = Library.Load();

        if(oldLibrary != curLibrary) {
            
            GameManager.instance.AddLibrary(curCoverImg); 
        } else {
            Debug.LogWarning("Library hasn't changed"); 
        }
        
    }

    public void GetMasterID(int _position) {
        curMasterID = masterResult.results[_position + ((curPage-1)*resultsPerPage)].master_id;
        
        uiManager.confirmReleaseMenu.SetActive(true);
    }

    public void GetReleaseID(int _position, Sprite _sprite) {
        curReleaseID = releaseResult.versions[_position + ((curPage-1)*resultsPerPage)].id;
        curCoverImg = _sprite;


        uiManager.saveReleaseMenu.SetActive(true);
    }

    void Update() {
        if(searched) {
            uiManager.searchedMenu.SetActive(true);
        } else {
            uiManager.searchedMenu.SetActive(false);
        }
    }
}

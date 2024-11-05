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
    public float searchingAnimDelay;
    
    [Header("Search Variables")]
    public int curPage = 1;
    public int curStartPage = 0;
    public string curType = "master";
    [HideInInspector] public int pageBuffer {get; private set;}
    public bool searched = false;
    [SerializeField] private int curMasterID;
    [SerializeField] private int curReleaseID;
    
    [Header ("Search Objects")]
    public List<GameObject> curSearchPreviews;
    public GameObject searchMasterPreviewPrefab;
    public GameObject searchReleasePreviewPrefab;
    [SerializeField] Transform searchResultsTrans;
    [SerializeField] Transform searchScrollContentTrans;
    
    [SerializeField] private Sprite curCoverImg;

    [Header ("Search Results")]
    public List<string> urls;
    public List<Texture2D> imgD;
    [SerializeField] public Discogs.Master masterResult = new (); 
    [SerializeField] public List<Discogs.Master> masterResultList = new ();
    [SerializeField] public Discogs.Release releaseResult = new ();
    [SerializeField] public List<Discogs.Release> releaseResultList = new ();
    [SerializeField] public Discogs.ReleaseInfo releaseInfo = new ();


    //Search Master based on the prompt given 
    public async void SearchMaster() {
        curType = "master";

        foreach (var t in curSearchPreviews) {
            Destroy(t);
        }
        curSearchPreviews.Clear();
        uiManager.StartCoroutine(uiManager.FirstSearchingAnimation());

        masterResultList.Clear();
        masterResult = await Discogs.Get.Masters(uiManager.searchPrompt.text, 1, searchResultsPerPage);
        masterResultList.Add(masterResult);

        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        curPage = 1;

        //Refresh Image URL's
        urls.Clear();

        for(int i = 0; i < masterResult.results.Length; i++) {
            urls.Add(masterResult.results[i+pageBuffer].cover_image);
        }
        imgD = await Discogs.Get.ImageList(urls);

        FirstSearchResults();
    }

    //Search Releases based on MasterID given
    public async void SearchRelease() {
        searched = false;
        curType = "release";

        foreach (var t in curSearchPreviews) {
            Destroy(t);
        }
        curSearchPreviews.Clear();

        uiManager.StartCoroutine(uiManager.FirstSearchingAnimation());
        releaseResult = await Discogs.Get.Releases(curMasterID, 1, searchResultsPerPage);
        releaseResultList.Add(releaseResult);
        
        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        curPage = 1;
        if (releaseResult.pagination.items > resultsPerPage) {
            uiManager.moreButton.interactable = true;
        }
        else
        {
            uiManager.moreButton.interactable = false;
        }  
        
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

        FirstSearchResults();
    }
    
    public void FirstSearchResults() {
        SendMessage("imstillalive");
        
        searched = true;
        curSearchPreviews.Clear();
        uiManager.StopCoroutine(uiManager.FirstSearchingAnimation());
        int resultsToLoad = resultsPerPage;

        switch (curType)
        {
            case "master":
            {

                if (resultsPerPage > masterResult.results.Length)
                {
                    resultsToLoad = masterResult.results.Length;
                }

                for (int i = 0; i < resultsToLoad; i++)
                {
                    curSearchPreviews.Add(Instantiate(searchMasterPreviewPrefab, searchResultsTrans, false));
                    SearchPreview _searchPreview = curSearchPreviews[i].GetComponent<SearchPreview>();
                    _searchPreview.curIndex = i;
                    _searchPreview.curPage = curPage;
                }

                break;
            }
            case "release":
            {
                if (resultsPerPage > releaseResult.versions.Length)
                {
                    resultsToLoad = releaseResult.versions.Length;
                }

                for (int i = 0; i < resultsToLoad; i++)
                {
                    curSearchPreviews.Add(Instantiate(searchReleasePreviewPrefab, searchResultsTrans, false));
                    ReleaseSearchPreview _searchPreview = curSearchPreviews[i].GetComponent<ReleaseSearchPreview>();
                    _searchPreview.curIndex = i;
                    _searchPreview.curPage = curPage;
                }

                break;
            }
        } 
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(searchResultsTrans.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(searchScrollContentTrans.GetComponent<RectTransform>());
        
        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        if(masterResult.pagination.items > curSearchPreviews.Count) { uiManager.moreButton.interactable = true; } else { uiManager.moreButton.interactable = false; }
    }

    private void AddResultsPrefabs()
    {
        searched = true;
        uiManager.StopCoroutine(uiManager.AddSearchingAnimation());
        int resultsToLoad = resultsPerPage;

        switch (curType)
        {
            case "master":
            {

                if (resultsPerPage > masterResult.results.Length)
                {
                    resultsToLoad = masterResult.results.Length - resultsPerPage;
                }

                for (int i = 0; i < resultsToLoad; i++)
                {
                    GameObject _newPrefriew = Instantiate(searchMasterPreviewPrefab, searchResultsTrans, false);
                    curSearchPreviews.Add(_newPrefriew);
                    SearchPreview _searchPreview = _newPrefriew.GetComponent<SearchPreview>();
                    _searchPreview.curIndex = i;
                    _searchPreview.curPage = curPage;
                }

                break;
            }
            case "release":
            {
                if(resultsPerPage > releaseResult.versions.Length)
                {
                    resultsToLoad = releaseResult.versions.Length - resultsPerPage;
                }
                for(int i = 0; i < resultsToLoad; i++) {
                    GameObject _newPrefriew = Instantiate(searchReleasePreviewPrefab, searchResultsTrans, false);
                    curSearchPreviews.Add(_newPrefriew);
                    ReleaseSearchPreview _searchPreview = _newPrefriew.GetComponent<ReleaseSearchPreview>();
                    _searchPreview.curIndex = i;
                    _searchPreview.curPage = curPage;
                }

                break;
            } 
                
        }  
        LayoutRebuilder.ForceRebuildLayoutImmediate(searchResultsTrans.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(searchScrollContentTrans.GetComponent<RectTransform>());
        
        //Reset UI buttons to the standard, making them clickable if there are more than results 'resultsPerPage'
        if(masterResult.pagination.items > curSearchPreviews.Count) { uiManager.moreButton.interactable = true; } else { uiManager.moreButton.interactable = false; }  
    }
    
    public async void AddResults()
    {
        searched = false;
        Debug.Log("Adding More Results...");
        uiManager.moreButton.gameObject.SetActive(false);
        curPage++;
        switch (curType)
        {
            case "master":
            {
                uiManager.StartCoroutine(uiManager.AddSearchingAnimation());
                masterResult = await Discogs.Get.Masters(uiManager.searchPrompt.text, curPage, searchResultsPerPage);
                masterResultList.Add(masterResult);
                
                //Refresh Image URL's
                urls.Clear();
                for(int i = 0; i < masterResult.results.Length; i++) {
                    urls.Add(masterResult.results[i+pageBuffer].cover_image);
                }
                break;
            }
            case "release":
            {   
                uiManager.StartCoroutine(uiManager.AddSearchingAnimation());
                releaseResult = await Discogs.Get.Releases(curMasterID, curPage, searchResultsPerPage);
                
                //Refresh Image URL's
                urls.Clear();
                for(int i = 0; i < releaseResult.versions.Length; i++) {
                    urls.Add(releaseResult.versions[i+pageBuffer].thumb);
                }
                break;
            }
        }
        
        imgD = await Discogs.Get.ImageList(urls);
        AddResultsPrefabs();
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
    public void GetMasterID(int _index, int _page) {
        curMasterID = masterResultList[_page-1].results[_index].master_id;
        uiManager.confirmReleaseMenu.SetActive(true);
    }
    
    //Selects the Release ID based on the index given
    public void GetReleaseID(int _index, int _page, Sprite _sprite) {
        curReleaseID = releaseResultList[_page-1].versions[_index].id;
        curReleaseID = releaseResult.versions[_index + ((curPage-1)*resultsPerPage)].id;
        curCoverImg = _sprite;
        uiManager.saveReleaseMenu.SetActive(true);
    }

    //Only enables the Next and back buttons if searched == true
    void Update() {
        if(searched) {
            uiManager.moreButton.gameObject.SetActive(true);
            uiManager.searchingText.enabled = false;
        } else{
            uiManager.moreButton.gameObject.SetActive(false);
        }
    }
}

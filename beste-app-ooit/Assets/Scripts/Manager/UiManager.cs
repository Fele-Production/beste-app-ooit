using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.Windows.Speech;
using Unity.VisualScripting.FullSerializer;

public class UIManager : MonoBehaviour {
    [Header("Search Settings")]
    public int resultsPerPage;
    public int searchResultsPerPage;

    [Header("Search Objects")]
    public GameObject searchMenu;
    public GameObject confirmReleaseMenu;
    private GameObject searchedMenu; 
    public TMP_InputField searchPrompt;
    public TMP_Text searchingText;
    public GameObject searchMasterPreviewPrefab;
    public GameObject searchReleasePreviewPrefab;
    public Button nextButton;
    public Button backButton;

    [Header ("Search Other")]
    public List<GameObject> curSearchPreviews;
    public List<Texture2D> imgD;
    public List<string> urls;
    public int curPage = 1;
    public string curType = "master";
    public float searchingAnimDelay;
    [HideInInspector] public int pageBuffer {get; private set;}
    public bool searched = false;
    private int curMasterID;

    [SerializeField] public Discogs.Master masterResult = new ();
    [SerializeField] public Discogs.Release releaseResult = new ();

    public async void SearchMaster() {
        curType = "master";
        StartCoroutine(SearchingAnimation());
        masterResult = await Discogs.Get.Masters(searchPrompt.text,1, resultsPerPage);
        
        curPage = 1;
        backButton.interactable = false;
        if(masterResult.results.Length > resultsPerPage) { nextButton.interactable = true; } else { nextButton.interactable = false; }  
        
        urls.Clear();

        for(int i = 0; i < masterResult.results.Length; i++) {
            urls.Add(masterResult.results[i+pageBuffer].cover_image);
        }
        imgD = await Discogs.Get.ImageList(urls);

        RefreshSearch();
    }

    public void NextPage() {
        curPage++;
        backButton.interactable = true;

        if(curType == "master") {
            if(masterResult.results.Length <= curPage * resultsPerPage) {
                nextButton.interactable = false;
            } 
        } else if(curType == "release") {
            if(releaseResult.versions.Length <= curPage * resultsPerPage) {
                nextButton.interactable = false;
            }
        }
             

        RefreshSearch();  
    }

    public void PreviousPage() {
        curPage--;
        nextButton.interactable = true;
        if(curPage == 1) {
            backButton.interactable = false;
        }   

        RefreshSearch();
    }


    public void RefreshSearch() {
        SendMessage("imstillalive");
        for(int i = 0; i < curSearchPreviews.Count; i++) {
            Destroy(curSearchPreviews[i]);
        }

        searched = true;
        curSearchPreviews.Clear();
        StopCoroutine(SearchingAnimation());
        int resultsToLoad = resultsPerPage;

        if(curType == "master") {
            if(resultsPerPage > (masterResult.results.Length - ((curPage-1) * resultsPerPage))) {
                resultsToLoad = masterResult.results.Length - ((curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                curSearchPreviews.Add(Instantiate(searchMasterPreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                curSearchPreviews[i].GetComponent<SearchPreview>().curPosition = i; 
                
            }
        } else if (curType == "release") {
            if(resultsPerPage > (releaseResult.versions.Length - ((curPage-1) * resultsPerPage))) {
                resultsToLoad = releaseResult.versions.Length - ((curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                    curSearchPreviews.Add(Instantiate(searchReleasePreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                    curSearchPreviews[i].GetComponent<ReleaseSearchPreview>().curPosition = i; 
                }
            
        }
    }     

    public void GetMasterID(int _position) {
        curMasterID = masterResult.results[_position + ((curPage-1)*resultsPerPage)].master_id;
        
        confirmReleaseMenu.SetActive(true);
    }

    public async void SearchRelease() {
        confirmReleaseMenu.SetActive(false);
        StartCoroutine(SearchingAnimation());
        releaseResult = await Discogs.Get.Releases(curMasterID, 1, searchResultsPerPage);
        
        curType = "release";
  
        
        
        curPage = 1;
        backButton.interactable = false;
        if(releaseResult.versions.Length > resultsPerPage) { nextButton.interactable = true; } else { nextButton.interactable = false; }  
        
        urls.Clear();

        for(int i = 0; i < releaseResult.versions.Length; i++) {
            urls.Add(releaseResult.versions[i+pageBuffer].thumb);
        }
        imgD = await Discogs.Get.ImageList(urls);

        RefreshSearch();
    }




    IEnumerator SearchingAnimation() {
        Debug.Log("Started Coroutine");
        searchingText.enabled = true;
        while (!searched) {
            searchingText.text = "Searching.. ";
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "Searching ..";
            yield return new WaitForSeconds(searchingAnimDelay);
            searchingText.text = "Searching. .";
            yield return new WaitForSeconds(searchingAnimDelay);
        }
        searchingText.enabled = false;
    }

    void Start() {
        searchedMenu = searchMenu.transform.Find("Searched Menu").gameObject;
        nextButton = searchedMenu.transform.Find("Forward").GetComponent<Button>();
        backButton = searchedMenu.transform.Find("Back").GetComponent<Button>();
    }

    void Update()
    {
        if(searched) {
            searchedMenu.SetActive(true);
        } else {
            searchedMenu.SetActive(false);
        }
    }
}

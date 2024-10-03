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

public class UiManager : MonoBehaviour {

    [Header("Search Settings")]
    public int resultsPerPage;
    public int resultsSearchedPerPage;

    [Header("Search Objects")]
    public GameObject searchMenu;
    private GameObject searchedMenu; 
    public TMP_InputField searchPrompt;
    public GameObject searchPreviewPrefab;
    public Button nextButton;
    public Button backButton;

    [Header ("Search Other")]
    public List<GameObject> curSearchPreviews;
    public List<Texture2D> imgD;
    public List<string> urls;
    [SerializeField] public Discogs.Master masterResult = new ();
    [SerializeField] public Discogs.Release releaseResult = new ();

    public int curPage = 1;
    [HideInInspector] public int pageBuffer {get; private set;}
    public bool searched = false;

    public async void Search() {
        masterResult = await Discogs.get.Masters(searchPrompt.text,1, resultsSearchedPerPage);
        Debug.Log(masterResult.results.Length);
        
        curPage = 1;
        backButton.interactable = false;
        if(masterResult.results.Length > resultsPerPage) { nextButton.interactable = true; } else { nextButton.interactable = false; }  
        
        urls.Clear();

        for(int i = 0; i < masterResult.results.Length; i++) {
            urls.Add(masterResult.results[i+pageBuffer].cover_image);
        }
        imgD = await Discogs.get.ImageList(urls);

        RefreshSearch();
    }

    public void NextPage() {
        curPage++;
        backButton.interactable = true;
        if(masterResult.results.Length <= curPage * resultsPerPage) {
            nextButton.interactable = false;
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
        for(int i = 0; i < curSearchPreviews.Count; i++) {
            Destroy(curSearchPreviews[i]);
        }
        curSearchPreviews.Clear();
        int resultsToLoad = resultsPerPage;
        if(resultsPerPage > (masterResult.results.Length - ((curPage-1) * resultsPerPage))) {
            resultsToLoad = masterResult.results.Length - ((curPage-1) * resultsPerPage);
        }
        for(int i = 0; i < resultsToLoad; i++) {
            curSearchPreviews.Add(Instantiate(searchPreviewPrefab, this.transform, false));
            curSearchPreviews[i].GetComponent<SearchPreview>().curPosition = i; 
        }

        searched = true;
    }


    public async void SelectMaster(int _position) {
        releaseResult = await Discogs.get.Releases(masterResult.results[_position + (curPage*resultsPerPage)].master_id, 1, resultsSearchedPerPage);
        
    }

    void Start()
    {
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

using System.Collections;
using System.Collections.Generic;
using Discogs;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    public List<GameObject> curAlbums;


    /*
    public void RefreshLibrary() {
        for(int i = 0; i < curAlbums.Count; i++) {
            Destroy(curAlbums[i]);
        }

        curAlbums.Clear();
        int resultsToLoad = resultsPerPage;

        if(searchManager.curType == "master") {
            if(resultsPerPage > (searchManager.masterResult.results.Length - ((searchManager.curPage-1) * resultsPerPage))) {
                resultsToLoad = searchManager.masterResult.results.Length - ((searchManager.curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                curAlbums.Add(Instantiate(searchMasterPreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                curAlbums[i].GetComponent<SearchPreview>().curPosition = i; 
                
            }
        } else if (searchManager.curType == "release") {
            if(resultsPerPage > (searchManager.releaseResult.versions.Length - ((searchManager.curPage-1) * resultsPerPage))) {
                resultsToLoad = searchManager.releaseResult.versions.Length - ((searchManager.curPage-1) * resultsPerPage);
            }
            for(int i = 0; i < resultsToLoad; i++) {
                    curAlbums.Add(Instantiate(searchReleasePreviewPrefab, searchMenu.transform.Find("SearchResults"), false));
                    curAlbums[i].GetComponent<ReleaseSearchPreview>().curPosition = i; 
                }
            
        }
    } */
}

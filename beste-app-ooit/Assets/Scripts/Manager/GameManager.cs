using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Discogs;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UserLibrary library;
    public GameObject albumPrefab;
    public List<GameObject> curAlbums;
    public int albumPerPage;
    public int curPage;

    void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //Initialize Data Files
        library = Library.Load();
        Game.Load();
        Settings.Load();
        RefreshLibrary();
    }

    public void RefreshLibrary() {
        library = Library.Load();
        
        for(int i = 0; i < curAlbums.Count; i++) {
            Destroy(curAlbums[i]);
        }

        curAlbums.Clear();
        int resultsToLoad = albumPerPage;

        for(int i = 0; i < library.Owned.Count; i++) {
            curAlbums.Add(Instantiate(albumPrefab, this.transform.Find("SearchResults"), false));
            curAlbums[i].GetComponent<ReleaseSearchPreview>().curPosition = i; 
        }         
    } 
}

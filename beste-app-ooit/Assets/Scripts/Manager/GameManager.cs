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
    public GameObject albumContent1;
    public GameObject albumContent2;
    public BigAlbumPreview bigAlbumPreview;
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
        Game.Load();
        Settings.Load();
        Debug.Log("Loaded everything");
        InitializeLibrary();
    }

    public void InitializeLibrary() {
        library = Library.Load();

        if(albumContent1 == null) {
            albumContent1 = GameObject.Find("Albums Content 1");
        } 
        if(albumContent2 == null) {
            albumContent2 = GameObject.Find("Albums Content 2"); 
        }
        
        for(int i = 0; i < curAlbums.Count; i++) {
            Destroy(curAlbums[i]);
        }

        curAlbums.Clear();
        int resultsToLoad = albumPerPage;

        for(int i = 0; i < library.Owned.Count; i++) {

            if(i % 2 == 0) {
                curAlbums.Add(Instantiate(albumPrefab, albumContent1.transform, false));
            } else {
                curAlbums.Add(Instantiate(albumPrefab, albumContent2.transform, false));
            }
            
            curAlbums[i].GetComponent<SmallAlbumPrefab>().curIndex = i; 
        }
    }

    public void AddLibrary(Sprite _sprite) {
        library = Library.Load();
        if((curAlbums.Count+1) % 2 == 0) {
            curAlbums.Add(Instantiate(albumPrefab, albumContent1.transform, false));
        } else {
            curAlbums.Add(Instantiate(albumPrefab, albumContent2.transform, false));
        }
        
        SmallAlbumPrefab curSmallAlbumPrefab = curAlbums[curAlbums.Count - 1].GetComponent<SmallAlbumPrefab>();
        curSmallAlbumPrefab.curIndex = curAlbums.Count-1;
        //curSmallAlbumPrefab.coverImage = _sprite;
    }


    public void SelectAlbum(int _index, Sprite _sprite) {
        bigAlbumPreview.enabled = true;
        bigAlbumPreview.gameObject.SetActive(true);
        bigAlbumPreview.curIndex = _index;
        bigAlbumPreview.curSprite = _sprite;
    }
}

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
    public GameObject albumChild;
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
        RefreshLibrary();
    }

    public void RefreshLibrary() {
        library = Library.Load();

        if(albumChild == null) {
            albumChild = GameObject.Find("AlbumsContent");
        }
        
        for(int i = 0; i < curAlbums.Count; i++) {
            Destroy(curAlbums[i]);
        }

        curAlbums.Clear();
        int resultsToLoad = albumPerPage;

        for(int i = 0; i < library.Owned.Count; i++) {
            curAlbums.Add(Instantiate(albumPrefab, albumChild.transform, false));
            curAlbums[i].GetComponent<SmallAlbumPrefab>().curPosition = i; 
        }         
    } 

    public void SelectAlbum(int _index, Sprite _sprite) {
        bigAlbumPreview.gameObject.SetActive(true);
        bigAlbumPreview.curIndex = _index;
        bigAlbumPreview.curSprite = _sprite;
    }
}

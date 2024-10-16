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
        InitializeLibrary();
    }

    public void InitializeLibrary() {
        library = Library.Load();
        Debug.Log(library.Owned.Count);

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

    public void AddLibrary(Sprite _sprite) {
        library = Library.Load();
        curAlbums.Add(Instantiate(albumPrefab, albumChild.transform, false));
        SmallAlbumPrefab curSmallAlbumPrefab = curAlbums[curAlbums.Count - 1].GetComponent<SmallAlbumPrefab>();
        curSmallAlbumPrefab.curPosition = curAlbums.Count-1;
        //curSmallAlbumPrefab.coverImage = _sprite;
    }


    public void SelectAlbum(int _index, Sprite _sprite) {
        bigAlbumPreview.enabled = true;
        bigAlbumPreview.gameObject.SetActive(true);
        bigAlbumPreview.curIndex = _index;
        bigAlbumPreview.curSprite = _sprite;
    }
}

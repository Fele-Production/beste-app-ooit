using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Discogs;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Variables")]
    public int albumPerPage;
    public int curPage;
    public float invokeDelay;
    public bool libraryChanged;
    [HideInInspector] public bool crRunning {get; private set;}

    [Header("Objects")]
    public GameObject albumPrefab;
    public GameObject albumContent1;
    public GameObject albumContent2;
    public BigAlbumPreview bigAlbumPreview;
    public List<GameObject> curAlbums;

    public UserLibrary library;
    
    public static GameManager instance;

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

        for(int i = 0; i < library.Owned.Count; i++) {

            if(i % 2 == 0) {
                curAlbums.Add(Instantiate(albumPrefab, albumContent1.transform, false));
            } else {
                curAlbums.Add(Instantiate(albumPrefab, albumContent2.transform, false));
            }
            
            curAlbums[i].GetComponent<SmallAlbumPrefab>().curIndex = i; 
        }

        Invoke("HomeCoroutineStart", invokeDelay);
    }

    public void AddLibrary(Sprite _sprite) {
        library = Library.Load();
        libraryChanged = true;

        if(curAlbums.Count % 2 == 0) {
            curAlbums.Add(Instantiate(albumPrefab, albumContent1.transform, false));
        } else {
            curAlbums.Add(Instantiate(albumPrefab, albumContent2.transform, false));
        }
        
        SmallAlbumPrefab curSmallAlbumPrefab = curAlbums[curAlbums.Count - 1].GetComponent<SmallAlbumPrefab>();
        curSmallAlbumPrefab.curIndex = curAlbums.Count-1;
        //curSmallAlbumPrefab.coverImage = _sprite;
    }

    public void RemoveLibrary(int _index) {
        library = Library.Load();
        Destroy(curAlbums[_index]);
        curAlbums.RemoveAt(_index);

        for (int i = _index; i < curAlbums.Count; i++) {
            curAlbums[i].GetComponent<SmallAlbumPrefab>().curIndex--;
        }

        StartCoroutine(HomeLayoutRefresh());
    }


    public void SelectAlbum(int _index, Sprite _sprite) {
        bigAlbumPreview.enabled = true;
        bigAlbumPreview.gameObject.SetActive(true);
        bigAlbumPreview.curIndex = _index;
        bigAlbumPreview.curSprite = _sprite;
    }

    private void HomeCoroutineStart() {
        StartCoroutine(HomeLayoutRefresh());
    }

    public IEnumerator HomeLayoutRefresh() {
        crRunning = true;

        Debug.Log("Refreshing Home...");
        yield return new WaitForEndOfFrame();

        LayoutRebuilder.ForceRebuildLayoutImmediate(albumContent1.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(albumContent2.GetComponent<RectTransform>());

        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(albumContent1.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(albumContent2.GetComponent<RectTransform>());

        crRunning = false;
    }
    
    public IEnumerator LayoutRefresh(RectTransform _content) {
        yield return new WaitForEndOfFrame(); 
        // Force the layout updates in the correct order
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content); // Rebuild parent (album content)
    }
}

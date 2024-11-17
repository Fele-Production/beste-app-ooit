using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Discogs;
using UnityEngine.UI;
using System.Threading.Tasks;

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

    void Awake() {
        //Make an instance of GameManager so that all scripts can access it easily
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else{
            Destroy(this.gameObject);
        }

        //Initialize Data Files
        Library.ReloadTextures();
        Game.ReloadTextures();
        InitializeLibrary();
        Game.Load();
        Settings.Load();
    }
    
    void Start() {
        Screen.SetResolution(Screen.width, Screen.height, false);
    }

    public async void InitializeLibrary() {
        Library.ReloadTextures();
        library = Library.Load();

        if(albumContent1 == null) {
            albumContent1 = GameObject.Find("Albums Content 1");
        } 
        if(albumContent2 == null) {
            albumContent2 = GameObject.Find("Albums Content 2"); 
        }
        
        foreach (var t in curAlbums)
        {
            Destroy(t);
        }

        curAlbums.Clear();

        //Places all the albums in the correct row
        for(int i = 0; i < library.Owned.Count; i++) {

            if(i % 2 == 0) {
                curAlbums.Add(Instantiate(albumPrefab, albumContent1.transform, false));
            } else {
                curAlbums.Add(Instantiate(albumPrefab, albumContent2.transform, false));
            }
            
            curAlbums[i].GetComponent<SmallAlbumPrefab>().curIndex = i; 
        }

        Invoke(nameof(HomeCoroutineStart), invokeDelay);
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
        //Enable the big album preview and give it the sprite (album cover) and curIndex
        bigAlbumPreview.gameObject.SetActive(true);
        bigAlbumPreview.curIndex = _index;
        bigAlbumPreview.curSprite = _sprite;
    }

    private void HomeCoroutineStart() {
        StartCoroutine(HomeLayoutRefresh());
    }

    public IEnumerator HomeLayoutRefresh() {
        Debug.Log("Refreshing home layout");
        //Refreshes the layout, so the UI is correctly displayed 
        crRunning = true;
        var content1Trans = albumContent1.GetComponent<RectTransform>();
        var content2Trans = albumContent2.GetComponent<RectTransform>();
        var sizeFitter1 = albumContent1.GetComponent<ContentSizeFitter>();
        var sizeFitter2 = albumContent2.GetComponent<ContentSizeFitter>();
        sizeFitter1.enabled = false;
        sizeFitter2.enabled = false;

        
        yield return new WaitForEndOfFrame();

        LayoutRebuilder.ForceRebuildLayoutImmediate(content1Trans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(content2Trans);
        
        sizeFitter1.enabled = true;
        sizeFitter2.enabled = true;
        crRunning = false;
    }
    
    public IEnumerator LayoutRefresh(RectTransform _content) {
        //Refreshes the layout, so the UI is correctly displayed 
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
    }
}

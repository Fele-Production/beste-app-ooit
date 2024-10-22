using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Discogs;
using System;


public class BigAlbumPreview : MonoBehaviour
{
    [Header("Settings")]
    public float delay;
    public float layoutDelay;

    [Header("Variables")]
    public int curIndex;
    private int discCount;
    private string genre;

    [Header("Objects")]
    public GameObject content;
    public RectTransform contentTransform;
    public Image cover;
    public TMP_Text titleText;
    public TMP_Text artistText;
    public TMP_Text otherInfo;
    public VerticalLayoutGroup contentVerticalLayout;
    public VerticalLayoutGroup tracksVerticalLayout;
    public ContentSizeFitter contentSizeFitter;
    public ContentSizeFitter trackSizeFitter;
    public GameObject trackPrefab;
    public List<GameObject> curTracks;
    public Sprite curSprite;

    
    

    
    private void OnEnable() 
    {
        content.SetActive(false);
        Invoke("Refresh", delay);     
    }

    public void Refresh() {
        UserLibrary curLibrary = GameManager.instance.library;
        if (curLibrary.Owned.Count != 0) {
            if(curSprite != null) {
                cover.sprite = curSprite;
            }
 
            if(curLibrary.Owned[curIndex].title != null) {
                string title = curLibrary.Owned[curIndex].title;
                titleText.text = title;
            } else {
                titleText.text = "---";
            }

            if(curLibrary.Owned[curIndex].artists[0].name != null) {
                artistText.text = curLibrary.Owned[curIndex].artists[0].name;
            } else {
                artistText.text = "---";
            }

            for(int i = 0; i < curTracks.Count; i++) {
                Destroy(curTracks[i]);
            }

            if(curLibrary.Owned[curIndex].tracklist != null) {
                discCount = CheckDiscCount(curLibrary);
                Debug.Log(discCount);
            

                curTracks.Clear();
                for (int i = 0; i < curLibrary.Owned[curIndex].tracklist.Length; i++) {
                    curTracks.Add(Instantiate(trackPrefab, content.transform.Find("Tracks")));
                    TrackPrefab _trackPrefab = curTracks[i].GetComponent<TrackPrefab>();
                    _trackPrefab.curAlbumIndex = curIndex;
                    _trackPrefab.curTrackIndex = i;

                }
            }

            if(curLibrary.Owned[curIndex].genres[0] != null) {
                genre = curLibrary.Owned[curIndex].genres[0];
            } else {
                genre = "";
            }

            otherInfo.text = genre + " || " + "Discs: " + discCount; 
            
        
        GameManager.instance.StartCoroutine(GameManager.instance.LayoutRefresh(tracksVerticalLayout.GetComponent<RectTransform>()));
        GameManager.instance.StartCoroutine(GameManager.instance.LayoutRefresh(contentTransform));
        }
        content.SetActive(true);    
    }

    private int CheckDiscCount(UserLibrary curLibrary) {
        int start = 'A';
        char letter = 'A';
        for (int i = 0; i <  curLibrary.Owned[curIndex].tracklist.Length; i++) {
            letter = curLibrary.Owned[curIndex].tracklist[i].position.ToCharArray()[0];
            if(!(letter >= 'A' && letter <= 'Z' || letter >= 'a' && letter <= 'z')) {
                return 0;
            }

            start = 'A';
            if(letter >= 'a' && letter <= 'z') {
                start = 'a';
            }
        }
        int discCount = (int)(((letter - start + 1) / 2) + 0.5f);
        return discCount;
    }

    public void RemoveAlbum() {
        int releaseID = GameManager.instance.library.Owned[curIndex].id;
        Library.Remove(releaseID);
        GameManager.instance.RemoveLibrary(curIndex);
    }

    
    private void OnDisable()
    {
        content.SetActive(false);
    }
}

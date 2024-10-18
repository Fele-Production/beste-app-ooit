using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Discogs;


public class BigAlbumPreview : MonoBehaviour
{
    [Header("Settings")]
    public float delay;
    public float layoutDelay;

    [Header("Variables")]
    public int curIndex;

    [Header("Objects")]
    public GameObject content;
    public RectTransform contentTransform;
    public Image cover;
    public TMP_Text titleText;
    public TMP_Text artistText;
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
        }
        content.SetActive(true);
        

        for(int i = 0; i < curTracks.Count; i++) {
            Destroy(curTracks[i]);
        }
        curTracks.Clear();
        for (int i = 0; i < curLibrary.Owned[curIndex].tracklist.Length; i++) {
            curTracks.Add(Instantiate(trackPrefab, content.transform.Find("Tracks")));
            TrackPrefab _trackPrefab = curTracks[i].GetComponent<TrackPrefab>();
            _trackPrefab.curAlbumIndex = curIndex;
            _trackPrefab.curTrackIndex = i;

        }
        
        StartCoroutine(RefreshLayout());
        //StartCoroutine(LayoutRefresh());
    }

    private IEnumerator RefreshLayout() {

        yield return new WaitForEndOfFrame(); 
        // Force the layout updates in the correct order
        LayoutRebuilder.ForceRebuildLayoutImmediate(tracksVerticalLayout.GetComponent<RectTransform>()); // Rebuild tracks first
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform); // Rebuild parent (album content)
    }

    private void OnDisable()
    {
        content.SetActive(false);
    }
}

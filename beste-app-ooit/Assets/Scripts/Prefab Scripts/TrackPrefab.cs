using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Discogs;
using System.Linq;


public class TrackPrefab : MonoBehaviour
{
    public TMP_Text trackNumberText;
    public TMP_Text trackTitleText;
    public TMP_Text durationText;
    public int curTrackIndex;
    public int curAlbumIndex;

    void Start() {
        UserLibrary curLibrary = GameManager.instance.library;

        if(curLibrary.Owned.Count != 0 && curLibrary.Owned[curAlbumIndex].tracklist != null) {
            if(curLibrary.Owned[curAlbumIndex].tracklist[curTrackIndex].title != null) {
                trackTitleText.text = curLibrary.Owned[curAlbumIndex].tracklist[curTrackIndex].title;
            } else {
                trackTitleText.text = "---";
            }

            if(curLibrary.Owned[curAlbumIndex].tracklist[curTrackIndex].position != null) {
                trackNumberText.text = curLibrary.Owned[curAlbumIndex].tracklist[curTrackIndex].position;
            } else {
                trackNumberText.text = (curTrackIndex + 1).ToString();
            }            
        }
    }
}

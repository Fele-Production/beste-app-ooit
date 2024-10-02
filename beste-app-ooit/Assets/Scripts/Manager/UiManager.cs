using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.Windows.Speech;

public class UiManager : MonoBehaviour {
    public TMP_InputField searchPrompt;
    public GameObject searchPreview;
    public List<GameObject> curSearchPreviews;
    public Image imgTest;
    public List<Texture2D> imgD;
    public List<string> urls;
    [SerializeField] public Discogs.Master jsonResult = new ();

    public async void Search() {
        jsonResult = await Discogs.get.Masters(searchPrompt.text,1,5);

        int oldUrlsCount = urls.Count;
        Debug.Log(oldUrlsCount);
        for (int i = 0; i < oldUrlsCount; i++) {
            urls.Remove(urls[0]);
        }


        for(int i = 0; i < 4; i++) {
            urls.Add(jsonResult.results[i].cover_image);
        }
        imgD = await Discogs.get.ImageList(urls);

        int oldsearchPreviewsCount = curSearchPreviews.Count;
        Debug.Log(oldsearchPreviewsCount);
        
        for(int i = 0; i < oldsearchPreviewsCount; i++) {
            Destroy(curSearchPreviews[0]);
            curSearchPreviews.Remove(curSearchPreviews[0]);
            Debug.Log("removed " + i);
        }
        
        for(int i = 0; i < 4; i++) {
            curSearchPreviews.Add(Instantiate(searchPreview, this.transform, false));
            curSearchPreviews[i].GetComponent<SearchPreview>().curPosition = i; 
        }
    }
}
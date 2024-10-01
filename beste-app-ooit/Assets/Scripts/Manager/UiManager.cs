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
    public Texture2D imgD;
    [SerializeField] public Discogs.Master jsonResult = new ();

    public async void Search() {
        jsonResult = await Discogs.get.Masters(searchPrompt.text,1,5);
        string url = jsonResult.results[0].cover_image;
        imgD = await Discogs.get.Image(url);
        
        for(int i = 0; i < curSearchPreviews.Count; i++) {
            Destroy(curSearchPreviews[i]);
            curSearchPreviews.Remove(curSearchPreviews[i]);
        }
        
        for(int i = 0; i < 4; i++) {
            curSearchPreviews.Add(Instantiate(searchPreview, this.transform, false));
            curSearchPreviews[i].GetComponent<SearchPreview>().curPosition = i; 
        }
    }
}
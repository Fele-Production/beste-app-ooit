using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class UiManager : MonoBehaviour {
    public TMP_InputField searchPrompt;
    public Image imgTest;
    public Texture2D imgD;

    public async void Search() {
        string url = (await Discogs.get.Masters(searchPrompt.text,1,1)).results[0].cover_image;
        StartCoroutine(Discogs.get.Image(url, callback => {if (callback != null){imgD=callback;}}));
        imgTest.sprite = Sprite.Create(imgD,new Rect(0,0,imgD.width,imgD.height), new Vector2(0,0));
    }
}
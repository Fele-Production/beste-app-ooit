using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

public class LibraryAccesser : MonoBehaviour {
    
    private ImageDownloader imgDownloader;
    [SerializeField] public Discogs.Master jsontest = new();

    private void Start() {
        imgDownloader = GetComponent<ImageDownloader>();
    }

    public async void Search(string search) {
        jsontest = await Discogs.get.Masters(search,1,5);
        imgDownloader.StartCoroutine(imgDownloader.DownloadImageFromURL(jsontest.results[0].cover_image));
    }

}

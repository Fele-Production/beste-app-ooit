using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Discogs;



public class SmallAlbumPrefab : MonoBehaviour
{
    [Header("Settings")]
    public int distance;
    public float invokeDelay;

    [Header("Variables")]
    public int curPosition;
    public int curDistance;
    public string url;

    [Header("Objects")]
    public TMP_Text titleText;
    public TMP_Text artistText;
    public Image cover;
    public VerticalLayoutGroup textLayoutGroup;

    public Sprite coverImage;
    
    
    public async void Start() {
        if(coverImage == null) {
            url = GameManager.instance.library.Owned[curPosition].image.resource_url;
            Texture2D _texture = await Discogs.Get.Image(url);
            coverImage = Sprite.Create(_texture,new Rect(0,0,_texture.width,_texture.height), new Vector2(0,0));
        }
        
        cover.sprite = coverImage;

        if (GameManager.instance.library.Owned.Count != 0) {
            if(GameManager.instance.library.Owned[curPosition].title != null) {
                    titleText.text = GameManager.instance.library.Owned[curPosition].title;
                    artistText.text = GameManager.instance.library.Owned[curPosition].artists[0].name;
                } else {
                titleText.text = "---";
            }

            StartCoroutine("VerticalLayoutRefresh");
        }
    }

    private IEnumerator VerticalLayoutRefresh() {
        yield return new WaitForSeconds(invokeDelay);
        textLayoutGroup.enabled = false;
        yield return new WaitForSeconds(invokeDelay);
        textLayoutGroup.enabled = true;
    }

    public void SelectAlbum() {
        GameManager.instance.SelectAlbum(curPosition, coverImage);
    }

}

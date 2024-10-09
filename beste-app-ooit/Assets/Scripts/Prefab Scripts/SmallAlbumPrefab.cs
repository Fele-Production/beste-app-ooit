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
    public TMP_Text titleText;
    public TMP_Text artistText;
    public Image cover;
    public int curPosition;
    public int distance;
    public int curDistance;

    public async void Awake() {
        string url = GameManager.instance.library.Owned[curPosition].image.resource_url;
        Texture2D _texture = await Discogs.Get.Image(url);
        cover.sprite = Sprite.Create(_texture,new Rect(0,0,_texture.width,_texture.height), new Vector2(0,0));


        if (GameManager.instance.library.Owned.Count != 0) {
            if(GameManager.instance.library.Owned[curPosition].title != null) {
                    titleText.text = GameManager.instance.library.Owned[curPosition].title;
                    artistText.text = GameManager.instance.library.Owned[curPosition].artists[0].name;
                } else {
                titleText.text = "---";
            }
        }
    
    }

}

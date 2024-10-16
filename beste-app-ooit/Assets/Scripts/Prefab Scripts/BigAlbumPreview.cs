using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BigAlbumPreview : MonoBehaviour
{

    public GameObject content;
    public Image cover;
    public TMP_Text titleText;
    public TMP_Text artistText;
    public GameObject trackPrefab;
    public Sprite curSprite;

    public int curIndex;
    public float delay;

    
    private void OnEnable() 
    {
        content.SetActive(false);
        Invoke("Refresh", delay);     
    }

    public void Refresh() {
        if (GameManager.instance.library.Owned.Count != 0) {
            if(curSprite != null) {
                cover.sprite = curSprite;
            }
 
            if(GameManager.instance.library.Owned[curIndex].title != null) {
                string title = GameManager.instance.library.Owned[curIndex].title;
                Debug.Log(title);
                titleText.text = title;
            } else {
                titleText.text = "---";
            }

            if(GameManager.instance.library.Owned[curIndex].artists[0].name != null) {
                artistText.text = GameManager.instance.library.Owned[curIndex].artists[0].name;
            } else {
                artistText.text = "---";
            }
        }
        content.SetActive(true);
    }

    private void OnDisable()
    {
        content.SetActive(false);
    }
}

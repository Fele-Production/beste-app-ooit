using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine.UI;
using UnityEngine;


public class BigAlbumPreview : MonoBehaviour
{
    private int curAlbum;
    public Image cover;
    public TMP_Text titleText;
    public TMP_Text artistText;
    public GameObject trackPrefab;
    public Sprite curSprite;

    public int curIndex;

    
    public void OnEnable() 
    {
        if (GameManager.instance.library.Owned.Count != 0) {
            if(curSprite != null)
 
            if(GameManager.instance.library.Owned[curIndex].title != null) {
                    titleText.text = GameManager.instance.library.Owned[curIndex].title;
                } else {
                titleText.text = "---";
            }

            if(GameManager.instance.library.Owned[curIndex].artists[0].name != null) {
                artistText.text = GameManager.instance.library.Owned[curIndex].artists[0].name;
            } else {
                artistText.text = "---";
            }
        }
    }
}

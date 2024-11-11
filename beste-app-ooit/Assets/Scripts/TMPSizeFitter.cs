using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TMPSizeFitter : MonoBehaviour
{
    [Header("Variables")]
    public float preferredHeight;
    
    [Header("Objects")]
    public TextMeshProUGUI text;
    public Transform contentTransform;
    public RectTransform textRectTransform;
    public RectTransform albumContentTransform;
    

    private void SetHeight() {
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight); 

        if(GameManager.instance.crRunning == false) {
            GameManager.instance.StartCoroutine(GameManager.instance.HomeLayoutRefresh());
        } 
    }
    
    private void Start() {
        if(text == null) {
            Debug.LogError("Text = null");
            return;
        }
        
        SmallAlbumPrefab smallAlbumPrefab = this.GetComponentInParent<SmallAlbumPrefab>();
        if(smallAlbumPrefab.curIndex % 2 == 0) {
            albumContentTransform = GameManager.instance.albumContent1.GetComponent<RectTransform>();
        } else {
            albumContentTransform = GameManager.instance.albumContent2.GetComponent<RectTransform>();
        }
        preferredHeight = text.preferredHeight;
        SetHeight();
        
    }

    private void Update() {
        if(preferredHeight != text.preferredHeight) {
            preferredHeight = text.preferredHeight;
            SetHeight();
        }
    }
}

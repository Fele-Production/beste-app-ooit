using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TMPSizeFitter : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Transform contentTransform;
    public RectTransform textRectTransform;
    public RectTransform albumContentTransform;
    public float preferredHeight;

    private void SetHeight() {
        if(text == null) {
            Debug.LogError("Text = null");
            return;
        }
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight); 

        if(GameManager.instance.crRunning == false) {
            GameManager.instance.StartCoroutine(GameManager.instance.HomeLayoutRefresh());
        } 

        //StartCoroutine(VerticalLayoutRefresh());
    }

    private IEnumerator VerticalLayoutRefresh() {
        yield return new WaitForEndOfFrame(); 
        // Force the layout updates in the correct order
        LayoutRebuilder.ForceRebuildLayoutImmediate(albumContentTransform); // Rebuild parent (album content)
    }

    private void Awake() {  
    }

    private void Start() {
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

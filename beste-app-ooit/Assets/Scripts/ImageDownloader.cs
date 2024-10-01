using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    public Sprite sprite;
    public Image imgTest;
    public string imgUrl;

    public IEnumerator DownloadImageFromURL(string url) {
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success) {
            Texture2D downloadedTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            Sprite curSprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0, 0));
            imgTest.sprite = curSprite;
        } else {
            Debug.LogError(request.error);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveHateLove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Discogs.ReleaseInfo releaseInfo = new();
    async void Start() {
        releaseInfo = await Discogs.get.ReleaseInfo(2499473);
    }
}

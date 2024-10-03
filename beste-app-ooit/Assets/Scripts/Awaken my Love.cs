using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Discogs;
using JetBrains.Annotations;
public class AwakenMyLove : MonoBehaviour {
    public UnityEngine.UI.Image image; 
    public Fantassimo daddy;
    void Awake() {
        daddy = GameObject.Find("Canvas").GetComponent<Fantassimo>();
    }
    void Start() {
        daddy.AddImage(image);
    }
}

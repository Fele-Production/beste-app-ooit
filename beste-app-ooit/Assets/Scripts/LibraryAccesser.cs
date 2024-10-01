using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;

public class LibraryAccesser : MonoBehaviour {

    [SerializeField] public Discogs.Master jsontest = new();
    async void Start() {
        jsontest = await Discogs.get.Masters("bleach",1,5);
    }

}

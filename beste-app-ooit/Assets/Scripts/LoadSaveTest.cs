using System.Collections;
using System.Collections.Generic;
using Discogs;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSaveTest : MonoBehaviour {
    ReleaseInfo ReleasetoSave;
    async void Start() {
        ReleasetoSave = await Get.ReleaseInfo(3742283);
        Library.Add(ReleasetoSave);

        //Debug.Log(Library.Load().Owned[0].title);
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Discogs;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static UserLibrary library;

    void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //Initialize Data Files
        library = Library.Load();
        Game.Load();
        Settings.Load();
    }

    public void SaveRelease(ReleaseInfo _releaseInfo) {
        Discogs.Library.Add(_releaseInfo);
    }

    public void RefreshLibrary() {
        library = Library.Load();
    }
}

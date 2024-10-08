using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discogs;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        Library.Load();
        Game.Load();
        Settings.Load();
    }

    public void SaveRelease(ReleaseInfo _releaseInfo) {
        Discogs.Library.Add(_releaseInfo);
    }
}

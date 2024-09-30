using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using UnityEngine;
using UnityEngine.Animations;

public class JSONREADERTEST : MonoBehaviour{
    public APItestGOATED apiTest;
    public string JSONfile;
    
    void Awake() {
        JSONfile = apiTest.finalResult;
    }


    [System.Serializable]
    public class Userdata {
        public bool in_wantlist;
        public bool in_collection;
    }
    [System.Serializable]
    public class Community_data {
        public int want;
        public int have;
    }
    [System.Serializable]
    public class Master {
        public string title;
        public string country;
        public string year;
        public string[] format;
        public string[] label;
        public string type;
        public int id;
        public string[] barcode;
        public Userdata user_data;
        public int master_id;
        public string master_url;
        public string uri;
        public string catno;
        public string thumb;
        public string cover_image;
        public string resource_url;
        public Community_data community;

    }
    [System.Serializable]
    public class Urls {
        public string last;
        public string next;
    }
    [System.Serializable]
    public class PagesInfo {
        public int page;
        public int pages;
        public int per_page;
        public int items;
        public Urls urls;
    }
    [System.Serializable]
    public class ResultJson {
        public PagesInfo pagination;
        public Master[] results;
    }

    public ResultJson jsontest = new ResultJson();

    void Start() { 
        jsontest = JsonUtility.FromJson<ResultJson>(JSONfile);
        Debug.Log(jsontest.pagination.pages);
    }
}

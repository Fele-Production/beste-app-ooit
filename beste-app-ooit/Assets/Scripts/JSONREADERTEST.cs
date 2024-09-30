using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using UnityEngine;

public class JSONREADERTEST : MonoBehaviour{
    public TextAsset JSONfile;

    public class userdata {
        public bool in_wantlist;
        public bool in_collection;
    }
    public class community_data {
        public int want;
        public int have;
    }
    public class master {
        public string country;
        public string year;
        public string[] format;
        public string[] label;
        public string type;
        public int id;
        public string[] barcode;
        public userdata user_data;
        public int master_id;
        public string master_url;
        public string uri;
        public string catno;
        public string title;
        public string thumb;
        public string cover_image;
        public string resource_url;
        public community_data community;

    }

    public class urls {
        public string last;
        public string next;
    }

    public class pagesInfo {
        public int page;
        public int pages;
        public int per_page;
        public int items;
        public urls urls;
    }

    public class resultJson {
        public pagesInfo pagination;
        public master[] results;
    }

    public resultJson jsontest = new resultJson();

    void Start() {
        jsontest = JsonUtility.FromJson<resultJson>(JSONfile.text);
        Debug.Log(jsontest.pagination.pages);
    }
}

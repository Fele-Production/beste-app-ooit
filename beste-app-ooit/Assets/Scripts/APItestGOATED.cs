using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;
using Unity.VisualScripting;

public class APItestGOATED : MonoBehaviour
{
    public string search;
    public string type;
    public string finalResult;

    void Start() {
        StartCoroutine(CallDiscogsAPI());
    }

    public IEnumerator CallDiscogsAPI() {
        yield return TestRawHttpWithVar();
    }
    
    public async Task TestRawHttpWithVar() {
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
        HttpClient client = new HttpClient(handler);

        // Add User-Agent header
        client.DefaultRequestHeaders.UserAgent.ParseAdd("PlaatFanaat/1.0");
        client.DefaultRequestHeaders.Add("Authorization", "Discogs token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");


        //check if Auth token is correctly configured
        HardCodedClientConfig config = new HardCodedClientConfig();
        if (config == null) {
            Debug.LogError("HardCodedClientConfig is null.");
            return;
        }

        if (string.IsNullOrEmpty(config.AuthToken)) {
            Debug.LogError("AuthToken is null or empty.");
            return;
        }
        Debug.Log("HardCodedClientConfig initialized correctly. AuthToken: " + config.AuthToken);
        
        // Check if the client is null
        if (client == null) {
            Debug.LogError("DiscogsClient is null. Check if the client was created correctly.");
            return;
        }
        Debug.Log("DiscogsClient initialized correctly");

        var searchFormat = "https://api.discogs.com/database/search?q="+search+"&type="+type+"?page=1&per_page=5";
        var response = await client.GetAsync(searchFormat+"&token=" + config.AuthToken);
        if (response.IsSuccessStatusCode) {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Debug.Log($"Raw Response: {jsonResponse}");
            finalResult = jsonResponse;
            JSONConvert();
        } else {
            Debug.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
        
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

    public void JSONConvert() {
        jsontest = JsonUtility.FromJson<ResultJson>(finalResult);
        Debug.Log(jsontest.pagination.pages);
    }
     
}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux";
        //public string UserAgent => "PlaatFanaat/1.0"; 
        public string BaseUrl => "https://api.discogs.com";
        
    } 



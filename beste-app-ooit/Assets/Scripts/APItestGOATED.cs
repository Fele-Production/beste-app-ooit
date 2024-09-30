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
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using ParkSquare.Discogs.Dto;


public class APItestGOATED : MonoBehaviour {
    //Var
    public string search;   //search query
    public string type;     //discogs type (master, release, artist, label)
    public int page;
    public int per_page;
    public int master_id;



    //Test API Code
    void Start() {
        StartCoroutine(CallDiscogsAPI());
    }
    //Discogs API Code
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

        //Search Type Definitions
        var searchFormat = "";
        var searchFormatMaster = $"https://api.discogs.com/database/search?q={search}&type=master?page={page}&per_page={per_page}";
        var searchFormatRelease = $"https://api.discogs.com/masters/{master_id}/versions?format=Vinyl&page={page}&per_page={per_page}";

        //Select Search Type
        if (type == "master") {searchFormat=searchFormatMaster;} //Requires: search, page, per_page
        else if (type == "release") {searchFormat=searchFormatRelease;} //Requires: master_id, page, per_page
        else Debug.LogError("Invalid/Unsupported Type");
        Debug.Log(searchFormat);
        Debug.Log(searchFormatMaster);
        var response = await client.GetAsync(searchFormat+"&token=" + config.AuthToken);
        if (response.IsSuccessStatusCode) {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Debug.Log($"Raw Response: {jsonResponse}");
            if (type == "master") {Debug.Log(ConvertJSONtoMaster(jsonResponse).pagination.pages);} //Test if Master search works
            if (type == "release") {Debug.Log(jsonResponse); jsontest = ConvertJSONtoRelease(jsonResponse);} //Test if Release Search works
        } else {
            Debug.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
        
    }
    //Master Class
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
    public class MasterJson {
        public PagesInfo pagination;
        public Master[] results;
    }

    //Release Class
    [System.Serializable]
    public class AppliedFilters {
        public string[] format;
    }
    
    [System.Serializable]
    public class AvailableFilters {

    }

    [System.Serializable]
    public class Filters {
        public AppliedFilters applied;
        public AvailableFilters available;
    }

    [System.Serializable]
    public class FilterFacetComponent {
        public string title;
        public string value;
        public int count;
    }

    [System.Serializable]
    public class FilterFacet {
        public string title;
        public string id;
        public FilterFacetComponent[] values;
        public bool allow_multiple_values;
    }

    [System.Serializable]
    public class ReleaseStats {
        public UserData community;
        public UserData user;
    }

    [System.Serializable]
    public class ReleaseVersion {
        public int id;
        public string label;
        public string country;
        public string title;
        public string[] major_formats;
        public string format;
        public string catno;
        public string released;
        public string status;
        public string resource_url;
        public string thumb;
        public ReleaseStats stats;
    }

    [System.Serializable]
    public class ReleaseJson {
        public PagesInfo pagination;
        public Filters filters;
        public FilterFacet[] filter_facets;
        public ReleaseVersion[] versions;
    }




    public ReleaseJson jsontest = new ReleaseJson(); //json test look nice :)

    public MasterJson ConvertJSONtoMaster(string jsonMasterInput) {
        return JsonUtility.FromJson<MasterJson>(jsonMasterInput);
    }

    public ReleaseJson ConvertJSONtoRelease(string jsonReleaseInput) {
        return JsonUtility.FromJson<ReleaseJson>(jsonReleaseInput);
    }
     
}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux";
        //public string UserAgent => "PlaatFanaat/1.0"; 
        public string BaseUrl => "https://api.discogs.com";
        
    } 









/* SEARCH QUERY THINGIES
___________MASTER___________________
query   --INCLUDED
string (optional) Example: nirvana
Your search query

type    --INCLUDED
string (optional) Example: release
String. One of release, master, artist, label

title
string (optional) Example: nirvana - nevermind
Search by combined “Artist Name - Release Title” title field.

release_title
string (optional) Example: nevermind
Search release titles.

credit
string (optional) Example: kurt
Search release credits.

artist
string (optional) Example: nirvana
Search artist names.

anv
string (optional) Example: nirvana
Search artist ANV.

label
string (optional) Example: dgc
Search label names.

genre
string (optional) Example: rock
Search genres.

style
string (optional) Example: grunge
Search styles.

country
string (optional) Example: canada
Search release country.

year
string (optional) Example: 1991
Search release year.

format
string (optional) Example: album
Search formats.

catno
string (optional) Example: DGCD-24425
Search catalog number.

barcode
string (optional) Example: 7 2064-24425-2 4
Search barcodes.

track
string (optional) Example: smells like teen spirit
Search track titles.

submitter
string (optional) Example: milKt
Search submitter username.

contributor
string (optional) Example: jerome99
Search contributor usernames.
__________RELEASES__________
master_id --INCLUDED
number (required) Example: 1000
The Master ID

page --INCLUDED
number (optional) Example: 3
The page you want to request

per_page --INCLUDED
number (optional) Example: 25
The number of items per page

format --HARDCODED
string (optional) Example: Vinyl
The format to filter

label
string (optional) Example: Scorpio Music
The label to filter

released
string (optional) Example: 1992
The release year to filter

country
string (optional) Example: Belgium
The country to filter

sort
string (optional) Example: released
Sort items by this field:
released (i.e. year of the release)
title (i.e. title of the release)
format
label
catno (i.e. catalog number of the release)
country

sort_order
string (optional) Example: asc
Sort items in a particular order (one of asc, desc)
*/
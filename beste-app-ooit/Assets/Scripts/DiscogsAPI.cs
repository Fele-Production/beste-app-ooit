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
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class DiscogsAPIFunc : MonoBehaviour {}

public class HardCodedClientConfig : IClientConfig {
    public string AuthToken => "QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux";
    //public string UserAgent => "PlaatFanaat/1.0"; 
    public string BaseUrl => "https://api.discogs.com";
} 


namespace Discogs {
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
    public class MasterItem {
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
    public class Master {
        public PagesInfo pagination;
        public MasterItem[] results;
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
    public class UserStats {
        public int in_wantlist;
        public int in_collection;
    }

    [System.Serializable]
    public class ReleaseStats {
        public UserStats community;
        public UserStats user;
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
    public class Release {
        public PagesInfo pagination;
        public Filters filters;
        public FilterFacet[] filter_facets;
        public ReleaseVersion[] versions;
    }
    //Functions
    public class ConvertJSON {
        public static Master Master(string jsonMasterInput) {
            return JsonUtility.FromJson<Master>(jsonMasterInput);
        }

        public static Release Release(string jsonReleaseInput) {
            return JsonUtility.FromJson<Release>(jsonReleaseInput);
        }
    }

    public class get {
        public static async Task<Master> Masters(string search, int page, int per_page) {
            //HTTP SetUp
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
                return null;
            }
            if (string.IsNullOrEmpty(config.AuthToken)) {
                Debug.LogError("AuthToken is null or empty.");
                return null;
            }
        
            // Check if the client is null
            if (client == null) {
                Debug.LogError("DiscogsClient is null. Check if the client was created correctly.");
                return null;
            }

            //Search Type Definition
            var searchFormatMaster = $"https://api.discogs.com/database/search?q={search}&type=master&page={page}&per_page={per_page}";
            //Search for Masters
            var Mresponse = await client.GetAsync(searchFormatMaster+"&token=" + config.AuthToken);
            if (Mresponse.IsSuccessStatusCode) {
                string jsonMResponse = await Mresponse.Content.ReadAsStringAsync();
                return ConvertJSON.Master(jsonMResponse);
            } else {
                Debug.LogError($"Error: {Mresponse.StatusCode} - {Mresponse.ReasonPhrase}");
                Debug.LogError("getMaster() failed");
                return null;
            }
        
        }

        public static async Task<Release> Releases(int master_id, int page, int per_page) {
            //HTTP SetUp
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
                return null;
            }
            if (string.IsNullOrEmpty(config.AuthToken)) {
                Debug.LogError("AuthToken is null or empty.");
                return null;
            }
        
            // Check if the client is null
            if (client == null) {
                Debug.LogError("DiscogsClient is null. Check if the client was created correctly.");
                return null;
            }

            //Search Type Definitions
            var searchFormatRelease = $"https://api.discogs.com/masters/{master_id}/versions?format=Vinyl&page={page}&per_page={per_page}";
            //Search for Releases
            var Rresponse = await client.GetAsync(searchFormatRelease+"&token=" + config.AuthToken);
            if (Rresponse.IsSuccessStatusCode) {
                string jsonRResponse = await Rresponse.Content.ReadAsStringAsync();
                return ConvertJSON.Release(jsonRResponse);
            } else {
                Debug.LogError($"Error: {Rresponse.StatusCode} - {Rresponse.ReasonPhrase}");
                return null;
            }
        
        }
    
        public static async Task<Texture2D> Image(string urlImage) {
            using (UnityWebRequest Irequest = UnityWebRequestTexture.GetTexture(urlImage)) {
                var operation = Irequest.SendWebRequest();

                while (!operation.isDone) {
                    await Task.Yield();
                }

                if (Irequest.result == UnityWebRequest.Result.Success) {
                    Texture2D _downloadedImg = DownloadHandlerTexture.GetContent(Irequest);
                    return _downloadedImg;
                } else {
                    Debug.LogError("Image Download Failed: " + Irequest.error);
                    return null;
                }
            }
        }
    }
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
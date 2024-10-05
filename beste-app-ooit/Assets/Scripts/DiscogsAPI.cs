using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.Extensions.Http;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.Timeline.Actions;
using System.Xml.Linq;

public class DiscogsAPIFunc : MonoBehaviour {}

public class ClientConfig {
    public string AuthToken => "ouFwPdyXIKjiYOTIIBrUliiYTKZfujQXCMVejGco";
    //public string UserAgent => "PlaatFanaat/1.0"; 
    public string BaseUrl => "https://api.discogs.com";
} 


namespace Discogs {
    public class ClassComponents {
        //Master Class Components
        [System.Serializable]
        public class Userdata {
            public bool in_wantlist;
            public bool in_collection;
        }

        [System.Serializable]
        public class CommunityData {
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
            public CommunityData community;
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

        //Release Class Components
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

        //Release Info Components
        [System.Serializable]
        public class Artist {
            public string name;
            public string anv;
            public string join; 
            public string role;
            public string tracks;
            public int id;
            public string resource_url;
            public string thumbnail_url;
        }

        [System.Serializable]
        public class Company {
            public string name;
            public string catno;
            public string entity_type;
            public string entity_type_name;
            public int id;
            public string resource_url;
            public string thumbnail_url;
        }

        [System.Serializable]
        public class Format {
            public string name;
            public string qty;
            public string[] descriptions;
        } 
        [System.Serializable]
        public class Rating {
            public int count;
            public float average;
        }
        [System.Serializable]
        public class CommunityDataAdvanced {
            public int have;
            public int want;
            public Rating rating;
            public string data_quality;
            public string status;
        }
        [System.Serializable]
        public class Identifier {
            public string type;
            public string value;
            public string description;
        }

        [System.Serializable]
        public class Video {
            public string uri;
            public string title;
            public string description;
            public int duration;
            public bool embed;
        }

        [System.Serializable]
        public class Track {
            public string position;
            public string type_;
            public string title;
            public Artist[] extraartists;
            public string duration;
        }

        [System.Serializable]
        public class ReleaseImage {
            public string type;
            public string uri;
            public string resource_url;
            public string uri150;
            public int width;
            public int height;
        }
    
    }

    //Important Classes
    [System.Serializable]
    public class Master {
        public ClassComponents.PagesInfo pagination;
        public ClassComponents.MasterItem[] results;
    }

    [System.Serializable]
    public class Release {
        public ClassComponents.PagesInfo pagination;
        public ClassComponents.FilterFacet[] filter_facets;
        public ClassComponents.ReleaseVersion[] versions;
    }

    [System.Serializable]
    public class ReleaseInfo {
        public int id;
        public string status;
        public int year;
        public string resource_url;
        public string uri;
        public ClassComponents.Artist[] artists;
        public string artists_sort;
        public ClassComponents.Company[] labels;
        //Series[]
        public ClassComponents.Company[] companies;
        public ClassComponents.Format[] formats;
        public string data_quality;
        public ClassComponents.CommunityDataAdvanced community;
        public int format_quantity;
        public int master_id;
        public string master_url;
        public string title;
        public string country;
        public string released; //1989-08-08
        public string notes;
        public string released_formatted; //08 Aug 1989
        public ClassComponents.Identifier[] identifiers;
        public ClassComponents.Video[] videos;
        public string[] genres;
        public string[] styles;
        public ClassComponents.Track[] tracklist;
        public ClassComponents.Artist[] extraartists;
        public ClassComponents.ReleaseImage[] images;
        public string thumb;
    }

    //Class for Saved Releases
    public class ReleaseLibrary {
        public List<ReleaseInfo> Owned = new();
        public List<ReleaseInfo> Wishlist = new();
    }
    //Functions
    public class Get {
        //Discogs Getters
        public static async Task<Master> Masters(string search, int page, int per_page) {
            //HTTP SetUp
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
            HttpClient client = new HttpClient(handler);

            // Add User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PlaatFanaat/1.0");
            client.DefaultRequestHeaders.Add("Authorization", "Discogs token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");

            //check if Auth token is correctly configured
            ClientConfig config = new ClientConfig();
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
                return JsonUtility.FromJson<Master>(jsonMResponse);
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
            ClientConfig config = new ClientConfig();
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
                return JsonUtility.FromJson<Release>(jsonRResponse);;
            } else {
                Debug.LogError($"Error: {Rresponse.StatusCode} - {Rresponse.ReasonPhrase}");
                return null;
            }
        
        }
    
        public static async Task<ReleaseInfo> ReleaseInfo(int release_id) {
            //HTTP SetUp
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
            HttpClient client = new HttpClient(handler);

            // Add User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("PlaatFanaat/1.0");
            client.DefaultRequestHeaders.Add("Authorization", "Discogs token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");

            //check if Auth token is correctly configured
            ClientConfig config = new ClientConfig();
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
            var searchFormatReleaseInfo = $"https://api.discogs.com/releases/{release_id}";
            //Search for Masters
            var RIresponse = await client.GetAsync(searchFormatReleaseInfo+"?token=" + config.AuthToken);
            if (RIresponse.IsSuccessStatusCode) {
                string jsonRIResponse = await RIresponse.Content.ReadAsStringAsync();
                return JsonUtility.FromJson<ReleaseInfo>(jsonRIResponse);
            } else {
                Debug.LogError($"Error: {RIresponse.StatusCode} - {RIresponse.ReasonPhrase}");
                Debug.LogError("getReleaseInfo() failed");
                return null;
            }
        }
        
        //Image Getters
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
        
        public static async Task<List<Texture2D>> ImageList(List<string> urlImages) {
            List<Texture2D> _downloadedImgs = new List<Texture2D>();

            for (int i = 0; i < urlImages.Count; i++) {
                Texture2D texture = await Image(urlImages[i]);
                if (texture != null) {
                    _downloadedImgs.Add(texture);
                } else {
                    Debug.LogWarning($"Failed to download image at URL: {urlImages[i]}");
                }
                _downloadedImgs.Add(null);
            }
            return _downloadedImgs;
        }
    }

    //Saved Info
    public class Library {
        public static string libPath = Application.persistentDataPath+"/Library.lox"; //l(evi) o(cean) (feli)x

        public static void Add(ReleaseInfo releaseToSave) {
            ReleaseLibrary _saveLibrary = Library.Load();
            _saveLibrary.Owned.Add(releaseToSave);
            File.WriteAllText(libPath,JsonUtility.ToJson(_saveLibrary,true));
        }
        
        public static void Remove (ReleaseInfo releaseToRemove) {
            ReleaseLibrary _removeLibrary = Library.Load();
            _removeLibrary.Owned.Remove(releaseToRemove);
            File.WriteAllText(libPath,JsonUtility.ToJson(_removeLibrary,true));
        }

        public static ReleaseLibrary Load() {
            string _LibraryStr;
            ReleaseLibrary _Library;
            if (File.Exists(libPath)) {
                _LibraryStr = File.ReadAllText(libPath);
            } else {
                _LibraryStr = "";
            }
            if (_LibraryStr.IsConvertibleTo<ReleaseLibrary>(true)) {
                _Library = JsonUtility.FromJson<ReleaseLibrary>(_LibraryStr);
            } else {
                _Library = new ReleaseLibrary();
            }
            return _Library;
        }
    
    }

}


/*
   public static void WriteString() {
       //Write some text to the test.txt file
       StreamWriter writer = new StreamWriter(path, true);
       writer.WriteLine("Test");
        writer.Close();
       StreamReader reader = new StreamReader(path);
       //Print the text from the file
       Debug.Log(reader.ReadToEnd());
       reader.Close();
    }
   public static void ReadString()
   {
       string path = Application.persistentDataPath + "/test.txt";
       //Read the text from directly from the test.txt file
       StreamReader reader = new StreamReader(path);
       Debug.Log(reader.ReadToEnd());
       reader.Close();
   }

 */







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
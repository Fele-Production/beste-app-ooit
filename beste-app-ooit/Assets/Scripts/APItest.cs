/*using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;

public class APItest : MonoBehaviour
{
    public string[] test;

    public void Start() {
        Debug.Log("Start");
        Main(test);
    }

    public static void Main(string[] args) {
        Debug.Log("Main Called");
        DiscogsClient client = new DiscogsClient( 
            new HttpClient(new HttpClientHandler()),
            new ApiQueryBuilder(new HardCodedClientConfig())); 

            var searchRes = client.SearchAsync(new SearchCriteria 
            {
                Artist = "$uicideboy$", 
                ReleaseTitle = "New World Depression"
            }).Result;
            Debug.Log(searchRes);
    }
}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "SuycForINtHDBKeVxxSGLQSsOtkjOeGpCdKBzENj";

        public string BaseUrl => "https://api.discogs.com";
    }
*/
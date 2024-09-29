
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;

public class APItest : MonoBehaviour {}
/* {

    public string[] test;

    public void Start() {
        Debug.Log("Start");
        Main(null);
    }

    public static async void Main(string[] args) {
        Debug.Log("Main Called");
        //DiscogsClient client = new DiscogsClient( 
            //new HttpClient(new HttpClientHandler()),
           // new ApiQueryBuilder(new HardCodedClientConfig())); 

        try{
            
            var searchRes = await client.SearchAsync(new SearchCriteria 
            {
                Artist = "Taylor Swift", 
                ReleaseTitle = "1989"
            });
            Debug.Log(searchRes);
        } catch (ArgumentNullException ex) {
        Debug.LogError("ArgumentNullException: " + ex.Message);
        } catch (Exception ex) {
        Debug.LogError("General Exception: " + ex.Message);
        }
        
    }
}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "SuycForINtHDBKeVxxSGLQSsOtkjOeGpCdKBzENj";

        public string BaseUrl => "https://api.discogs.com";
    }
*/
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;

public class APItestGOATED : MonoBehaviour
{
    public string search;
    public string type;
    public void Start() {
        StartCoroutine(CallDiscogsAPI());
    }

    private IEnumerator CallDiscogsAPI() {
        yield return TestRawHttpWithVar();
    }
        

    private async Task TestRawHttpWithVar() {
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

        var searchFormat = "https://api.discogs.com/database/search?q="+search+"&type="+type;
        var response = await client.GetAsync(searchFormat+"&token=" + config.AuthToken);
        if (response.IsSuccessStatusCode) {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Debug.Log("https://api.discogs.com/database/search?" + search + "&token=" + config.AuthToken);
            Debug.Log($"Raw Response: {jsonResponse}");
        } else {
            Debug.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    }
     
}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux";
        //public string UserAgent => "PlaatFanaat/1.0"; 
        public string BaseUrl => "https://api.discogs.com";
        
    } 



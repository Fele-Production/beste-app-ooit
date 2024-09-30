using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;

public class ApiTestChatgpt : MonoBehaviour
{
    public string search;
    public string artistSearch;
    public string releaseSearch;

    public void Start() {
        StartCoroutine(CallDiscogsAPI());
    }

    private IEnumerator CallDiscogsAPI() {
        /* Task discogsTask = GetDiscogsData();
        while (!discogsTask.IsCompleted) {
            yield return null;  // Wait until the async task is done
        }
        if (discogsTask.Exception != null) {
            Debug.LogError("Task failed: " + discogsTask.Exception);
        }

        yield return TestRawHttpRequest(); */

        yield return TestRawHttpWithVar();
    }
        

    private async Task GetDiscogsData() {
        
        HttpClientHandler handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
        HttpClient httpClient = new HttpClient(handler);

         // Add User-Agent header
        httpClient.DefaultRequestHeaders.Add("User-Agent", "PlaatFanaat/1.0");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Discogs token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");
        Debug.Log($"User-Agent: {httpClient.DefaultRequestHeaders.UserAgent}");


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


        ApiQueryBuilder apiQueryBuilder = new ApiQueryBuilder(config);
        DiscogsClient client = new DiscogsClient(httpClient, apiQueryBuilder);
        
        // Check if the client is null
        if (client == null) {
            Debug.LogError("DiscogsClient is null. Check if the client was created correctly.");
            return;
        }
        Debug.Log("DiscogsClient initialized correctly");

        
        //try {
            SearchCriteria criteria = new SearchCriteria {Artist = "Taylor Swift", ReleaseTitle = "1989"}; 
            Debug.Log($"Search Criteria: Artist={criteria.Artist}, ReleaseTitle={criteria.ReleaseTitle}");

            // Step 4: Perform API Search

            var searchRes = await client.SearchAsync(criteria);

            // Check response
            if (searchRes == null) {
            Debug.LogError("API call returned null. Check your query or API token.");
            return;
            }

            Debug.Log("Search successful. Response: " + JsonUtility.ToJson(searchRes));
            if (searchRes.Pagination != null) {
                Debug.Log($"Found {searchRes.Pagination.Items} items.");
            } else {
                Debug.LogError("Pagination is null in the search result.");
            }
        /*} catch (Exception ex) {
            Debug.LogError($"Exception during API call: {ex.Message}");
        }*/
    }

    private async Task TestRawHttpRequest() {
    HttpClientHandler handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
    HttpClient client = new HttpClient(handler);

    // Add User-Agent header
    client.DefaultRequestHeaders.UserAgent.ParseAdd("PlaatFanaat/1.0");
    client.DefaultRequestHeaders.Add("Authorization", "Discogs token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");
    
    try {
        var response = await client.GetAsync("https://api.discogs.com/database/search?1989&token=QQyCaJSIXCsCErdlhaXQMSoEXYOCORMtOYOSqbux");
        if (response.IsSuccessStatusCode) {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Debug.Log($"Raw Response: {jsonResponse}");
        } else {
            Debug.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        }
    } catch (Exception ex) {
        Debug.LogError($"Exception in raw HTTP request: {ex.Message}");
    }
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

        //string searchUrl = $"artist={Uri.EscapeDataString(artistSearch)}&release_title={Uri.EscapeDataString(releaseSearch)}";
        
        var response = await client.GetAsync("https://api.discogs.com/database/search?" + search + "&token=" + config.AuthToken);
        if (response.IsSuccessStatusCode) {
            string jsonResponse = await response.Content.ReadAsStringAsync();
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



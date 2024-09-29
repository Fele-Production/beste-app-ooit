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
    public void Start() {
        StartCoroutine(CallDiscogsAPI());
    }

    private IEnumerator CallDiscogsAPI() {
        yield return GetDiscogsData();
    }

    private async Task GetDiscogsData() {
        Debug.Log("Get Discogs Data called");

        HttpClient httpClient = new HttpClient(new HttpClientHandler());
        ApiQueryBuilder apiQueryBuilder = new ApiQueryBuilder(new HardCodedClientConfig());
        DiscogsClient client = new DiscogsClient(httpClient, apiQueryBuilder);
        Debug.Log($"Using AuthToken: {new HardCodedClientConfig().AuthToken}");
        Debug.Log($"Client: {client}");
        Debug.Log($"SearchCriteria Artist: Taylor Swift, ReleaseTitle: 1989");


        if (client == null) Debug.LogError("DiscogsClient is null");
        //if (client.SearchAsync == null) Debug.LogError("SearchAsync method is null");

        //try {
            var searchRes = await client.SearchAsync(new SearchCriteria 
            {
                Artist = "Taylor Swift",
                ReleaseTitle = "1989"
            });
            if (searchRes == null) {
                Debug.LogError("API call returned null. Check your query or API token.");
            } else {
                Debug.Log("Search result: " + searchRes);
            }
        /*} catch (HttpRequestException ex) {
            Debug.LogError("HttpRequestException: " + ex.Message);
        } catch (Exception ex) {
            Debug.LogError("General Exception: " + ex.Message);
        } */

        
    }

}

public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "SuycForINtHDBKeVxxSGLQSsOtkjOeGpCdKBzENj";

        public string BaseUrl => "https://api.discogs.com";
        
    }



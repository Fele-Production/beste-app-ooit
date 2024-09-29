using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

using UnityEngine;
using UnityEngine.Networking;
using ParkSquare.Discogs;
using Microsoft.Extensions.Http;
using DiscogsDemo;

namespace DiscogsDemo
{
    public class Program : MonoBehaviour{
        public static string Main(/*string[] args*/)
        {
            var client = new DiscogsClient(
                new HttpClient(new HttpClientHandler()),
                new ApiQueryBuilder(new HardCodedClientConfig()));

            var searchResult = client.SearchAllAsync(new SearchCriteria
            {
                Artist = "2Pac",
                ReleaseTitle = "All Eyez On Me"
            }).Result;
            /*var masterId = 123456;
            var master = client.GetMasterReleaseAsync(masterId).Result;

            var versions = client.GetAllVersionsAsync(new VersionsCriteria(84819)).Result;

            var release = client.GetReleaseAsync(238369).Result;

            var now15 = client.GetReleaseAsync(1890799).Result;
            var media = now15.Tracklist.SplitMedia();*/
            return searchResult.ToString();
        }            
    }

    public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "SuycForINtHDBKeVxxSGLQSsOtkjOeGpCdKBzENj";

        public string BaseUrl => "https://api.discogs.com";
    }
}
public class testtest : MonoBehaviour {
    void Start () {
        Debug.Log(Program.Main());
    }
}
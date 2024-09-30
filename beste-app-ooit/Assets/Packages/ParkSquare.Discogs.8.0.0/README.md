# C# Discogs Library

[![Build Status](https://dev.azure.com/parksq/parksq/_apis/build/status/Discogs?repoName=Discogs&branchName=master)](https://dev.azure.com/parksq/parksq/_build/latest?definitionId=89&repoName=Discogs&branchName=master)

## Background

[Discogs](https://www.discogs.com/about) is a fully cross-referenced music database of recordings, releases, artists, 
labels and album artwork. Discogs provides a RESTful API to access this data, and this C# library is the easiest way 
to consume it so you can build your own Discogs-powered applications for the web, desktop, and mobile devices.

## Discogs Masters & Releases

The Discogs database is organized into 'masters' and 'releases'. The 'master' is the main database record for a particular
recording (such as an album). A 'master' can have multiple 'releases', which are variations of the 'master' record. 
For example, an album may have been released on CD, cassette and vinyl, with different versions in the US Europe. This
would give that particular master six different releases. The tracklists may, or may not, differ between releases.

**Example Master**
[2Pac - All Eyez On Me (Master)](https://www.discogs.com/master/84819-2Pac-All-Eyez-On-Me)

**Example Releases**
[2Pac - All Eyez On Me, 4 x Vinyl (Release)](https://www.discogs.com/release/238369-2Pac-All-Eyez-On-Me)
[2Pac - All Eyez On Me, Double CD (Release)](https://www.discogs.com/release/4882196-2Pac-All-Eyez-On-Me)

## Getting Started

Sign up to get a Discogs account and go to [Developer Settings](https://www.discogs.com/settings/developers) to create 
a new auth token. Not all endpoints require authentication, however, the rate limit is higher for authenticated users
so it will always be passed if you have configured one.

## Rate Limits

The rate limit is generally around 60 requests per minute if authenticated, or 25 requests per minute if not. Once
this limit has been reached, further calls will respond with `429 Too Many Requests`. You should therefore make sure
your code handles this, either with some sort of wait/retry or a circuit breaker. 

See the Advanced section below for examples of gracefully handling rate limiting. 

## Paged Results

Some of the API calls return paged data, with a maximum of 100 items per page. As such, you will need to either handle
these scenarios yourself, or use the built-in convenience methods that automatically handle getting the paged results
and presenting them back as one combined dataset.

## Configuration

You must create an implementation of `IClientConfig` and pass this into the constructor of the `ApiQueryBuilder` class.
The recommended way is to use the .Net Core built-in dependency injection framework, and to bind the properties to your 
`appsettings.json` file:

In `Startup.cs`:

    services.AddSingleton<IClientConfig, ClientConfig>();

In your application, create a config class:

    public class ClientConfig : IClientConfig
    {
        public ClientConfig(IConfiguration configuration)
        {
            configuration.Bind("DiscogsClient", this);
        }

        public string AuthToken { get; set; }

        public string BaseUrl { get; set; }
    }

In `appsettings.json`:

    {
      "DiscogsClient": {
        "AuthToken": "your-auth-token-here",
        "BaseUrl": "https://api.discogs.com"
      }
    }

This method is not mandatory, you can pass in any implementation of the configuration interface, for example
the values could just as easily be hardcoded: 

    public class HardCodedClientConfig : IClientConfig
    {
        public string AuthToken => "your-auth-token";

        public string BaseUrl => "https://api.discogs.com";
    }

## Usage Examples

### Discogs Client

All calls are made via the `DiscogsClient` object, which can be instantiated manually or using Dependency Injection
(recommended).

**Manual construction**

    var client = new DiscogsClient(
        new HttpClient(new HttpClientHandler()),
        new ApiQueryBuilder(new ClientConfig()));

**Dependency injection**

Add these lines to your `Startup.cs`:

    services.AddSingleton<IApiQueryBuilder, ApiQueryBuilder>();

    services.AddSingleton<IClientConfig, ClientConfig>();

    services.AddHttpClient<IDiscogsClient, DiscogsClient>()
        .ConfigurePrimaryHttpMessageHandler(_ =>
            new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

The last line registers a typed `HttpClient` for the `DiscogsClient` to use, and requires the following package: 

    Microsoft.Extensions.Http

There is a detailed discussion on using dependency injection with `HttpClient` [here](https://www.parksq.co.uk/dotnet-core/dependency-injection-httpclient-and-ihttpclientfactory).

### Music Search

There are three methods for searching the database, each one will return matching releases from the Discogs database. Each of these releases will have a ReleaseId and also the 
MasterId of the master record that it is linked to.

    Task<SearchResults> SearchAsync(SearchCriteria criteria);
    Task<SearchResults> SearchAsync(SearchCriteria criteria, PageOptions pageOptions);  
    Task<SearchResults> SearchAllAsync(SearchCriteria criteria);

The `SearchAsync()` methods allow specifying search criteria and optional pagination options. If no pagination options 
are passed, then the defaults will be used which returns the first page up to a maximum of 100 items. 

Calling `SearchAllAsync()` automatically handles making multiple calls if necessary to retrieve all items, and the combining of the paged results into one dataset.
By default, a maximum of four concurrent pages will be retrieved simultaneously from Discogs, however you can change this value 
by setting the `MaxConcurrentRequests` property on the `DiscogsClient` object.

    var searchResult = await _client.SearchAllAsync(new SearchCriteria
    {
        Artist = "2Pac",
        ReleaseTitle = "All Eyez On Me"
    });

The available search fields in full are as follows, with examples:

- **Artist** Pet Shop Boys
- **ReleaseTitle** Use Your Illusion II
- **Title** Queen - Greatest Hits
- **Query** cornershop
- **Year** 1998
- **Track** Coma
- **Barcode** 111234
- **CatalogNumber** XASIOIJH
- **Country** UK
- **Format** CD

Note that using 'Query' is akin to performing a free text search and 'Title' is generally in the format 'Artist - Title'.

### Get Master Release 

The master release can be retrieved using the MasterId returned by a search. This will contain the ReleaseId of whichever release is considered to be the 'main' one, usually the one 
that is chronologically the earliest. It will also provide the ReleaseId of the most recent (i.e. chronologically latest) release that has been linked to that master.

    var master = await _client.GetMasterReleaseAsync(masterId);

### Get Release Versions

The version-related methods return all releases that are linked to the master. 

    Task<VersionResults> GetVersionsAsync(VersionsCriteria criteria);
    Task<VersionResults> GetVersionsAsync(VersionsCriteria criteria, PageOptions pageOptions);
    Task<VersionResults> GetAllVersionsAsync(VersionsCriteria criteria);

Similar to Search, this method has optional pagination properties. You can omit the pagination properties to get the first page of 100 records, or call `GetAllVersionsAsync()` 
to automatically handle retrieving and combining paged results.

    var versions = await _client.GetAllVersionsAsync(new VersionsCriteria(84819));

You can further refine the versions returned using filters for format, label, release year and country. The response will give you a summary of how many items match each available filter.

### Get Release

The release record is where the bulk of the information about a recording is held, including a link back to the MasterId. 

    var release = await _client.GetReleaseAsync(238369);

### Release Media

A release may have tracks arranged over one or more pieces of physical media, for example [Now That's What I Call Music 15](https://www.discogs.com/release/1890799-Various-Now-Thats-What-I-Call-Music-15) 
has 32 tracks on two CDs. Other examples include multiple vinyl sets, double cassette albums, special editions and flat lists of digital audio files. These are presented by Discogs in various ways, 
depending on how they were added to the database. A convenient extension method is shipped as part of this library to split the tracks according to the media they are on:

    var now15 = await _client.GetReleaseAsync(1890799);
    var media = now15.Tracklist.SplitMedia();

This method returns a dictionary with a media identifier as the key and a list of tracks as the value.

# Advanced Configuration

## Handling Rate Limiting

You can use the excellent [Polly](https://www.nuget.org/packages/Polly) library to automatically add retries and a circuit 
breaker pattern to your Discogs calls. This example exponentially backs off making calls and then gives up after three attempts.
It will also break the circuit and stop making calls altogether if 3 failures are returned within a 15 second period.

    services.AddHttpClient<IDiscogsClient, DiscogsClient>()
        .AddPolicyHandler(Policy.Handle<HttpRequestException>()
            .OrTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt))))
        .AddPolicyHandler(Policy.Handle<HttpRequestException>()
            .OrTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(15)
            ))
        .ConfigurePrimaryHttpMessageHandler(_ =>
            new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

# Complete Example

Here is a basic example to get you started, that does not use async or dependency injection.

    using ParkSquare.Discogs;

    namespace DiscogsDemo
    {
        class Program {
            static void Main(string[] args)
            {
                var client = new DiscogsClient(
                    new HttpClient(new HttpClientHandler()),
                    new ApiQueryBuilder(new HardCodedClientConfig()));

                var searchResult = client.SearchAllAsync(new SearchCriteria
                {
                    Artist = "2Pac",
                    ReleaseTitle = "All Eyez On Me"
                }).Result;

                var masterId = 123456;
                var master = client.GetMasterReleaseAsync(masterId).Result;

                var versions = client.GetAllVersionsAsync(new VersionsCriteria(84819)).Result;

                var release = client.GetReleaseAsync(238369).Result;

                var now15 = client.GetReleaseAsync(1890799).Result;
                var media = now15.Tracklist.SplitMedia();
            }            
        }

        public class HardCodedClientConfig : IClientConfig
        {
            public string AuthToken => "your-auth-token";

            public string BaseUrl => "https://api.discogs.com";
        }
    }


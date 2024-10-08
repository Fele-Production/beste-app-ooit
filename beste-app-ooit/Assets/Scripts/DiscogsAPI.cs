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


namespace Discogs {
    public static class GlobalVariables {
        //public readonly static string declarationofindependenceoftheunitedstatesofamerica = "theunanimousdeclarationofthethirteenunitedstatesofamericawheninthecourseofhumaneventsitbecomesnecessaryforonepeopletodissolvethepoliticalbandswhichhaveconnectedthemwithanotherandtoassumeamongthepowersoftheearththeseparateandequalstationtowhichthelawsofnatureandofnaturesgodentitlethemadecentrespecttotheopinionsofmankindrequiresthattheyshoulddeclarethecauseswhichimpelthemtotheseparationweholdthesetruthstobeselfevidentthatallmenarecreatedequalthattheyareendowedbytheircreatorwithcertainunalienablerightsthatamongthesearelifelibertyandthepursuitofhappinessthattosecuretheserightsgovernmentsareinstitutedamongmenderivingtheirjustpowersfromtheconsentofthegovernedthatwheneveranyformofgovernmentbecomesdestructiveoftheseendsitistherightofthepeopletoalterortoabolishitandtoinstitutenewgovernmentlayingitsfoundationonsuchprinciplesandorganizingitspowersinsuchformastothemshallseemmostlikelytoeffecttheirsafetyandhappinessprudenceindeedwilldictatethatgovernmentslongestablishedshouldnotbechangedforlightandtransientcausesandaccordinglyallexperiencehathshewnthatmankindaremoredisposedtosufferwhileevilsaresufferablethantorightthemselvesbyabolishingtheformstowhichtheyareaccustomedbutwhenalongtrainofabusesandusurpationspursuinginvariablythesameobjectevincesadesigntoreducethemunderabsolutedespotismitistheirrightitistheirdutytothrowoffsuchgovernmentandtoprovidenewguardsfortheirfuturesecuritysuchhasbeenthepatientsufferanceofthesecoloniesandsuchisnowthenecessitywhichconstrainsthemtoaltertheirformersystemsofgovernmentthehistoryofthepresentkingofgreatbritainisahistoryofrepeatedinjuriesandusurpationsallhavingindirectobjecttheestablishmentofanabsolutetyrannyoverthesestatestoprovethisletfactsbesubmittedtoacandidworldhehasrefusedhisassenttolawsthemostwholesomeandnecessaryforthepublicgoodhehasforbiddenhisgovernorstopasslawsofimmediateandpressingimportanceunlesssuspendedintheiroperationtillhisassentshouldbeobtainedandwhensosuspendedhehasutterlyneglectedtoattendtothemhehasrefusedtopassotherlawsfortheaccommodationoflargedistrictsofpeopleunlessthosepeoplewouldrelinquishtherightofrepresentationinthelegislaturearightinestimabletothemandformidabletotyrantsonlyhehascalledtogetherlegislativebodiesatplacesunusualuncomfortableanddistantfromthedepositoryoftheirpublicrecordsforthesolepurposeoffatiguingthemintocompliancewithhismeasureshehasdissolvedrepresentativehousesrepeatedlyforopposingwithmanlyfirmnesshisinvasionsontherightsofthepeoplehehasrefusedforalongtimeaftersuchdissolutionstocauseotherstobeelectedwherebythelegislativepowersincapableofannihilationhavereturnedtothepeopleatlargefortheirexercisethestateremaininginthemeantimeexposedtoallthedangersofinvasionfromwithoutandconvulsionswithinhehasendeavouredtopreventthepopulationofthesestatesforthatpurposeobstructingthelawsfornaturalizationofforeignersrefusingtopassotherstoencouragetheirmigrationshitherandraisingtheconditionsofnewappropriationsoflandshehasobstructedtheadministrationofjusticebyrefusinghisassenttolawsforestablishingjudiciarypowershehasmadejudgesdependentonhiswillaloneforthetenureoftheirofficesandtheamountandpaymentoftheirsalarieshehaserectedamultitudeofnewofficesandsenthitherswarmsofofficerstoharrassourpeopleandeatouttheirsubstancehehaskeptamongusintimesofpeacestandingarmieswithouttheconsentofourlegislatureshehasaffectedtorenderthemilitaryindependentofandsuperiortothecivilpowerhehascombinedwithotherstosubjectustoajurisdictionforeigntoourconstitutionandunacknowledgedbyourlawsgivinghisassenttotheiractsofpretendedlegislationforquarteringlargebodiesofarmedtroopsamongusforprotectingthembyamocktrialfrompunishmentforanymurderswhichtheyshouldcommitontheinhabitantsofthesestatesforcuttingoffourtradewithallpartsoftheworldforimposingtaxesonuswithoutourconsentfordeprivingusinmanycasesofthebenefitsoftrialbyjuryfortransportingusbeyondseastobetriedforpretendedoffencesforabolishingthefreesystemofenglishlawsinaneighbouringprovinceestablishingthereinanarbitrarygovernmentandenlargingitsboundariessoastorenderitatonceanexampleandfitinstrumentforintroducingthesameabsoluteruleintothesecoloniesfortakingawayourchartersabolishingourmostvaluablelawsandalteringfundamentallytheformsofourgovernmentsforsuspendingourownlegislaturesanddeclaringthemselvesinvestedwithpowertolegislateforusinallcaseswhatsoeverhehasabdicatedgovernmentherebydeclaringusoutofhisprotectionandwagingwaragainstushehasplunderedourseasravagedourcoastsburntourtownsanddestroyedthelivesofourpeopleheisatthistimetransportinglargearmiesofforeignmercenariestocompleattheworksofdeathdesolationandtyrannyalreadybegunwithcircumstancesofcruelty&perfidyscarcelyparalleledinthemostbarbarousagesandtotallyunworthytheheadofacivilizednationhehasconstrainedourfellowcitizenstakencaptiveonthehighseastobeararmsagainsttheircountrytobecometheexecutionersoftheirfriendsandbrethrenortofallthemselvesbytheirhandshehasexciteddomesticinsurrectionsamongstusandhasendeavouredtobringontheinhabitantsofourfrontiersthemercilessindiansavageswhoseknownruleofwarfareisanundistinguisheddestructionofallagessexesandconditionsineverystageoftheseoppressionswehavepetitionedforredressinthemosthumbletermsourrepeatedpetitionshavebeenansweredonlybyrepeatedinjuryaprincewhosecharacteristhusmarkedbyeveryactwhichmaydefineatyrantisunfittobetherulerofafreepeoplenorhavewebeenwantinginattentionstoourbrittishbrethrenwehavewarnedthemfromtimetotimeofattemptsbytheirlegislaturetoextendanunwarrantablejurisdictionoveruswehaveremindedthemofthecircumstancesofouremigrationandsettlementherewehaveappealedtotheirnativejusticeandmagnanimityandwehaveconjuredthembythetiesofourcommonkindredtodisavowtheseusurpationswhichwouldinevitablyinterruptourconnectionsandcorrespondencetheytoohavebeendeaftothevoiceofjusticeandofconsanguinitywemustthereforeacquiesceinthenecessitywhichdenouncesourseparationandholdthemasweholdtherestofmankindenemiesinwarinpeacefriendswethereforetherepresentativesoftheunitedstatesofamericaingeneralcongressassembledappealingtothesupremejudgeoftheworldfortherectitudeofourintentionsdointhenameandbyauthorityofthegoodpeopleofthesecoloniessolemnlypublishanddeclarethattheseunitedcoloniesareandofrightoughttobefreeandindependentstatesthattheyareabsolvedfromallallegiancetothebritishcrownandthatallpoliticalconnectionbetweenthemandthestateofgreatbritainisandoughttobetotallydissolvedandthatasfreeandindependentstatestheyhavefullpowertolevywarconcludepeacecontractalliancesestablishcommerceandtodoallotheractsandthingswhichindependentstatesmayofrightdoandforthesupportofthisdeclarationwithafirmrelianceontheprotectionofdivineprovidencewemutuallypledgetoeachotherourlivesourfortunesandoursacredhonor";
        public readonly static string FileExtension = "skibidi";
        public readonly static string libPath = $"{Application.persistentDataPath}/UserData.{FileExtension}"; //C:\Users\(USER)\AppData\LocalLow\Fele Productions\beste-app-ooit
        public readonly static string setPath = $"{Application.persistentDataPath}/UserSettings.{FileExtension}";
        public readonly static string gamePath = $"{Application.persistentDataPath}/GameData.{FileExtension}";
    }

    public static class ClientConfig {
        public static string AuthToken => "ouFwPdyXIKjiYOTIIBrUliiYTKZfujQXCMVejGco";
        public static string UserAgent => "PlaatFanaat/0.1"; 
        public static string BaseUrl => "https://api.discogs.com";
    } 

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

        //Release Library
        [System.Serializable]
        public class UserTheme {
            public bool Fantassimo = false;
            //Add whatever the fuck you can customize
        }
        public class UserSettings {
            public bool PerformanceMode = false;
            //Again whatever the fuck
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

    [System.Serializable]
    public class ReleaseInfoOptimized {
        public int id;
        public int year;
        public ClassComponents.Artist[] artists;
        public string artists_sort;
        public ClassComponents.Company[] labels;
        public ClassComponents.Format[] formats;
        public int master_id;
        public string title;
        public string country;
        public string released; //1989-08-08
        public string notes;
        public string released_formatted; //08 Aug 1989
        public string[] genres;
        public string[] styles;
        public ClassComponents.ReleaseImage image;
    }

    //Class for Saved Releases
    [System.Serializable]
    public class UserLibrary {
        public List<ReleaseInfoOptimized> Owned = new();
        public List<ReleaseInfoOptimized> Wishlist = new();
    }
    //Class for Game Data
    [System.Serializable]
    public class GameData {
        public List<ReleaseInfoOptimized> Owned = new();
        public List<ReleaseInfoOptimized> Wishlist = new();
        public float Experience;
    }

    [System.Serializable]
    public class UserSettings {
        public ClassComponents.UserTheme Theme = new();
        public ClassComponents.UserSettings Settings = new();
    }


    //Functions
    public static class Get {
        //Discogs Getters
        public static async Task<Master> Masters(string search, int page, int per_page) {
            //HTTP SetUp
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true; // For debugging SSL issues
            HttpClient client = new HttpClient(handler);

            // Add User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd(ClientConfig.UserAgent);
            client.DefaultRequestHeaders.Add("Authorization", $"Discogs token={ClientConfig.AuthToken}");

            //check if Auth token is correctly ClientConfigured
            if (string.IsNullOrEmpty(ClientConfig.AuthToken)) {
                Debug.LogError("AuthToken is null or empty.");
                return null;
            }

            //Search Type Definition
            var searchFormatMaster = $"{ClientConfig.BaseUrl}/database/search?q={search}&type=master&page={page}&per_page={per_page}";
            //Search for Masters
            var Mresponse = await client.GetAsync(searchFormatMaster+"&token=" + ClientConfig.AuthToken);
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
            client.DefaultRequestHeaders.UserAgent.ParseAdd(ClientConfig.UserAgent);
            client.DefaultRequestHeaders.Add("Authorization", $"Discogs token={ClientConfig.AuthToken}");

            //check if Auth token is correctly ClientConfigured
            if (string.IsNullOrEmpty(ClientConfig.AuthToken)) {
                Debug.LogError("AuthToken is null or empty.");
                return null;
            }

            //Search Type Definitions
            var searchFormatRelease = $"{ClientConfig.BaseUrl}/masters/{master_id}/versions?format=Vinyl&page={page}&per_page={per_page}";
            //Search for Releases
            var Rresponse = await client.GetAsync(searchFormatRelease+"&token=" + ClientConfig.AuthToken);
            if (Rresponse.IsSuccessStatusCode) {
                string jsonRResponse = await Rresponse.Content.ReadAsStringAsync();
                return JsonUtility.FromJson<Release>(jsonRResponse);
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
            client.DefaultRequestHeaders.UserAgent.ParseAdd(ClientConfig.UserAgent);
            client.DefaultRequestHeaders.Add("Authorization", $"Discogs token={ClientConfig.AuthToken}");

            //check if Auth token is correctly ClientConfigured
            if (string.IsNullOrEmpty(ClientConfig.AuthToken)) {
                Debug.LogError("AuthToken is null or empty.");
                return null;
            }

            //Search Type Definition
            var searchFormatReleaseInfo = $"{ClientConfig.BaseUrl}/releases/{release_id}";
            //Search for Masters
            var RIresponse = await client.GetAsync(searchFormatReleaseInfo+"?token=" + ClientConfig.AuthToken);
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
    public static class Library {
        public static void Add(ReleaseInfo releaseToSave) {
            UserLibrary _saveLibrary = Library.Load();
            _saveLibrary.Owned.Add(Convert.OptimizeReleaseInfo(releaseToSave));
            File.WriteAllText(GlobalVariables.libPath,JsonUtility.ToJson(_saveLibrary,true));
        }
        
        public static void Remove (ReleaseInfo releaseToRemove) {
            UserLibrary _removeLibrary = Library.Load();
            _removeLibrary.Owned.Remove(Convert.OptimizeReleaseInfo(releaseToRemove));
            File.WriteAllText(GlobalVariables.libPath,JsonUtility.ToJson(_removeLibrary,true));
        }

        public static UserLibrary Load() {
            string _LibraryStr;
            UserLibrary _Library;
            if (File.Exists(GlobalVariables.libPath)) {
                _LibraryStr = File.ReadAllText(GlobalVariables.libPath);
            } else {
                _LibraryStr = "";
                File.Create(GlobalVariables.libPath);
            }
            if (_LibraryStr.IsConvertibleTo<UserLibrary>(true)) {
                _Library = JsonUtility.FromJson<UserLibrary>(_LibraryStr);
            } else {
                _Library = new UserLibrary();
            }
            return _Library;
        }

        public static void HardSave(UserLibrary _saveLibrary) {
            File.WriteAllText(GlobalVariables.libPath,JsonUtility.ToJson(_saveLibrary,true));
        }
        

        public static class Wishlist {

            public static void Add(ReleaseInfo releaseToSave) {
                UserLibrary _saveWishlist = Library.Load();
                _saveWishlist.Wishlist.Add(Convert.OptimizeReleaseInfo(releaseToSave));
                File.WriteAllText(GlobalVariables.libPath,JsonUtility.ToJson(_saveWishlist,true));
            }
        
            public static void Remove (ReleaseInfo releaseToRemove) {
                UserLibrary _removeWishlist = Library.Load();
                _removeWishlist.Wishlist.Remove(Convert.OptimizeReleaseInfo(releaseToRemove));
                File.WriteAllText(GlobalVariables.libPath,JsonUtility.ToJson(_removeWishlist,true));
            }
        }
    }

    public static class Game {

        public static void Add(ReleaseInfo releaseToSave) {
            GameData _saveGameData = Game.Load();
            _saveGameData.Owned.Add(Convert.OptimizeReleaseInfo(releaseToSave));
            File.WriteAllText(GlobalVariables.gamePath,JsonUtility.ToJson(_saveGameData,true));
        }
        
        public static void Remove (ReleaseInfo releaseToRemove) {
            GameData _removeGameData = Game.Load();
            _removeGameData.Owned.Remove(Convert.OptimizeReleaseInfo(releaseToRemove));
            File.WriteAllText(GlobalVariables.gamePath,JsonUtility.ToJson(_removeGameData,true));
        }

        public static GameData Load() {
            string _GameDataStr;
            GameData _GameData;
            if (File.Exists(GlobalVariables.gamePath)) {
                _GameDataStr = File.ReadAllText(GlobalVariables.gamePath);
            } else {
                _GameDataStr = "";
                File.Create(GlobalVariables.gamePath);
            }
            if (_GameDataStr.IsConvertibleTo<GameData>(true)) {
                _GameData = JsonUtility.FromJson<GameData>(_GameDataStr);
            } else {
                _GameData = new GameData();
            }
            return _GameData;
        }

        public static void HardSave(GameData _saveGameData) {
            File.WriteAllText(GlobalVariables.gamePath,JsonUtility.ToJson(_saveGameData,true));
        }

        public static class Wishlist {

            public static void Add(ReleaseInfo releaseToSave) {
                GameData _saveWishlist = Game.Load();
                _saveWishlist.Wishlist.Add(Convert.OptimizeReleaseInfo(releaseToSave));
                    File.WriteAllText(GlobalVariables.gamePath,JsonUtility.ToJson(_saveWishlist,true));
            }
        
            public static void Remove (ReleaseInfo releaseToRemove) {
                GameData _removeWishlist = Game.Load();
                _removeWishlist.Wishlist.Remove(Convert.OptimizeReleaseInfo(releaseToRemove));
                File.WriteAllText(GlobalVariables.gamePath,JsonUtility.ToJson(_removeWishlist,true));
            }
        }
    }

    public static class Settings {
        public static UserSettings Load() {
            string _SettingsStr;
            UserSettings _Settings;
            if (File.Exists(GlobalVariables.setPath)) {
                _SettingsStr = File.ReadAllText(GlobalVariables.setPath);
            } else {
                _SettingsStr = "";
                File.Create(GlobalVariables.setPath);
            }
            if (_SettingsStr.IsConvertibleTo<UserSettings>(true)) {
                _Settings = JsonUtility.FromJson<UserSettings>(_SettingsStr);
            } else {
                _Settings = new UserSettings();
            }
            return _Settings;
        }
        public static void SaveTheme(bool? IFantassimo) {
            UserSettings _themeSettings = Load();
            if (IFantassimo.HasValue) {_themeSettings.Theme.Fantassimo = IFantassimo.Value;}
            File.WriteAllText(GlobalVariables.setPath,JsonUtility.ToJson(_themeSettings,true));
        }

        public static void SaveSettings(bool? IPerformanceMode) {
            UserSettings _Settings = Load();
            if (IPerformanceMode.HasValue) {_Settings.Settings.PerformanceMode = IPerformanceMode.Value;}
            File.WriteAllText(GlobalVariables.setPath,JsonUtility.ToJson(_Settings,true));
        }
    }

    public static class Convert {
        public static ReleaseInfoOptimized OptimizeReleaseInfo(ReleaseInfo releaseInfoInput) {
            ReleaseInfoOptimized _releaseInfoInputOpt = JsonUtility.FromJson<ReleaseInfoOptimized>(JsonUtility.ToJson(releaseInfoInput));
            _releaseInfoInputOpt.image = releaseInfoInput.images[0];
            return _releaseInfoInputOpt;
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
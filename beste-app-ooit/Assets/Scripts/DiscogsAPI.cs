using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;




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

        //Settings/Theme Components
        [System.Serializable]
        public class UserTheme {
            public bool Fantassimo = false;
            public int Backdrop = 0; //0=blue 1=beige 2=green
            //Add whatever the fuck you can customize
        }

        [System.Serializable]
        public class UserSettings {
            public bool PerformanceMode = false;
            //Again whatever the fuck
        }
    }

    //Settings/Theme Components
    public class InputTheme {
        public bool? Fantassimo;
        public int? Backdrop;
    }

    public class InputSettings {
        public bool? PerformanceMode;
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
        public ClassComponents.Track[] tracklist;
        public ClassComponents.ReleaseImage image;
    }

    //Class for Saved Releases
    [System.Serializable]
    public class UserLibrary {
        public List<ReleaseInfoOptimized> Owned = new();
        public List<ReleaseInfoOptimized> Wishlist = new();

        public bool Contains(ReleaseInfo relCheck,bool InWishlist = false) {
            if (!InWishlist) {
                foreach (var rel in Owned) {
                    if (rel.id == relCheck.id) {
                        return true;
                    }
                }
                return false;
            } else {
                foreach (var rel in Wishlist) {
                    if (rel.id == relCheck.id) {
                        return true;
                        }
                    }
                return false;
            }
        }
    }
    //Class for Game Data
    [System.Serializable]
    public class GameData : UserLibrary {
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

        public static Texture2D ImageFromPath(string path) {
            var rawData = System.IO.File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(0,0);
            ImageConversion.LoadImage(tex,rawData);
            return tex;
        }
    }

    //Saved Info
    public static class Library {
        public static void Add(ReleaseInfo releaseToSave,bool multiples_allowed = false) {
            FileModification.Add<UserLibrary>(GlobalVariables.libPath,false,releaseToSave,multiples_allowed);
        }
        public static void Remove(int releaseToRemoveID) {
            FileModification.Remove<UserLibrary>(GlobalVariables.libPath,false,releaseToRemoveID);
        }
        public static UserLibrary Load() {
            return FileModification.Load<UserLibrary>(GlobalVariables.libPath);
        }
        public static string LoadStr() {
            return FileModification.LoadStr(GlobalVariables.libPath);
        }
        public static void HardSave(UserLibrary libraryToSave) {
            FileModification.HardSave(GlobalVariables.libPath,libraryToSave);
        }
        public static bool Contains(ReleaseInfo relToCheck) {
            return FileModification.Contains(Load().Owned,relToCheck);
        }

        public static class Wishlist {
            public static void Add(ReleaseInfo releaseToSave, bool multiples_allowed = false) {
            FileModification.Add<UserLibrary>(GlobalVariables.libPath,true,releaseToSave,multiples_allowed);
            }

            public static void Remove(int releaseToRemoveID) {
                FileModification.Remove<UserLibrary>(GlobalVariables.libPath,true,releaseToRemoveID);
            }

            public static bool Contains(ReleaseInfo relToCheck) {
                return FileModification.Contains(Load().Wishlist,relToCheck);
            }
        }
        
    }

    public static class Game {
        public static void Add(ReleaseInfo releaseToSave, bool multiples_allowed = false) {
            FileModification.Add<GameData>(GlobalVariables.gamePath,false,releaseToSave, multiples_allowed);
        }
        public static void Remove(int releaseToRemoveID) {
            FileModification.Remove<GameData>(GlobalVariables.gamePath,false,releaseToRemoveID);
        }
        public static GameData Load() {
            return FileModification.Load<GameData>(GlobalVariables.gamePath);
        }
        public static string LoadStr() {
            return FileModification.LoadStr(GlobalVariables.gamePath);
        }
        public static void HardSave(GameData libraryToSave) {
            FileModification.HardSave(GlobalVariables.gamePath,libraryToSave);
        }
        public static bool Contains(ReleaseInfo relToCheck) {
            return FileModification.Contains(Load().Owned,relToCheck);
        }

        public static class Wishlist {
            public static void Add(ReleaseInfo releaseToSave, bool multiples_allowed = false) {
            FileModification.Add<GameData>(GlobalVariables.gamePath,true,releaseToSave, multiples_allowed);
            }
            public static void Remove(int releaseToRemoveID) {
                FileModification.Remove<GameData>(GlobalVariables.gamePath,true,releaseToRemoveID);
            }

            public static bool Contains(ReleaseInfo relToCheck) {
                return FileModification.Contains(Load().Wishlist,relToCheck);
            }
        }
        
    }
    
    public static class Settings {
        public static UserSettings Load() {
            return FileModification.Load<UserSettings>(GlobalVariables.setPath);
        }
        public static void SaveTheme(bool? IFantassimo, int? IBackdrop) {
            UserSettings _themeSettings = Load(); 
            if (IFantassimo.HasValue) {_themeSettings.Theme.Fantassimo = IFantassimo.Value;}
            if (IBackdrop.HasValue) {_themeSettings.Theme.Backdrop = IBackdrop.Value;}
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

    public static class FileModification {
        public static void Add<inputType>(string path, bool Wishlist, ReleaseInfo itemToSave, bool multiples_allowed) where inputType : UserLibrary,new() {
            inputType _saveLibrary = Load<inputType>(path);
            if (!Wishlist) {
                if (_saveLibrary.Contains(itemToSave)) {
                    if (multiples_allowed) {
                        _saveLibrary.Owned.Add(Convert.OptimizeReleaseInfo(itemToSave));
                    }
                } else {
                    _saveLibrary.Owned.Add(Convert.OptimizeReleaseInfo(itemToSave));
                }
            } else {
                if (_saveLibrary.Contains(itemToSave)) {
                    if (multiples_allowed) {
                        _saveLibrary.Wishlist.Add(Convert.OptimizeReleaseInfo(itemToSave));
                    }
                } else {
                    _saveLibrary.Wishlist.Add(Convert.OptimizeReleaseInfo(itemToSave));
                }
            }
            HardSave(path,_saveLibrary);
        }
        
        public static void Remove<inputType>(string path, bool Wishlist, int releaseToRemoveID) where inputType : UserLibrary,new() {
            inputType _removeLibrary = Load<inputType>(path);
            if (!Wishlist) {
                foreach (ReleaseInfoOptimized rel in _removeLibrary.Owned) {
                    if (rel.id == releaseToRemoveID) {
                        _removeLibrary.Owned.Remove(rel);
                        break;
                    }
                }
            } else {
                foreach (ReleaseInfoOptimized rel in _removeLibrary.Wishlist) {
                    if (rel.id == releaseToRemoveID) {
                        _removeLibrary.Wishlist.Remove(rel);
                        break;
                    }
                }
            }
            HardSave(path,_removeLibrary);
        }

        public static inputType Load<inputType>(string path) where inputType : class, new(){
            string _LibraryStr;
            inputType _Library;
            if ((typeof(inputType) != typeof(UserLibrary))&&(typeof(inputType) != typeof(GameData))&&(typeof(inputType) != typeof(UserSettings))) {
                Debug.LogError($"Invalid Type for Template: {typeof(inputType).Name}");
                return default;
            }
            
            if (File.Exists(path)) {
                _LibraryStr = File.ReadAllText(path);
            } else {
                _LibraryStr = "";
                File.Create(path);
            }
            _Library = JsonUtility.FromJson<inputType>(_LibraryStr);
            if (_Library == null) {
                Debug.LogWarning($"Trying to convert incompatible JSON to {typeof(inputType)}, returning new()");
                return new inputType();
            }
            return _Library;
        }
        
        public static string LoadStr(string path) {
            string _LibraryStr;
            if (File.Exists(path)) {
                _LibraryStr = File.ReadAllText(path);
            } else {
                _LibraryStr = "";
                File.Create(path);
            }
            return _LibraryStr;
        }

        public static void HardSave(string path,object _saveLibrary) {
            File.WriteAllText(path,JsonUtility.ToJson(_saveLibrary,true));
        }
        
        public static bool Contains(List<ReleaseInfoOptimized> rels,ReleaseInfo relCheck) {
            foreach (var rel in rels) {
                if (rel.id == relCheck.id) {
                    return true;
                }
            }
            return false;
        }
        
        private static void ConditionalSave<inputType>(string path, bool Theme, inputType inputClass) where inputType : class,new(){ //WIP
            UserSettings saveLib = Load<UserSettings>(path);
            Type classType = typeof(inputType);

            if (!Theme) {
                foreach (var field in classType.GetFields()) {
                    var fieldValue = field.GetValue(inputClass);
                    if (field.GetValue(inputClass) != null) {
                        field.SetValue(saveLib.Settings, fieldValue);
                    }
                }
            } else {
                foreach (var field in classType.GetFields()) {
                    var fieldValue = field.GetValue(inputClass);
                    if (fieldValue != null) {
                        field.SetValue(saveLib.Theme, fieldValue);
                    }
                }
            }

            HardSave(path,saveLib);
        }
        
        /*
        public static void Add() {
            FileModification.Add();
        }
        public static void Remove() {
            FileModification.Remove();
        }
        public static UserLibrary Load() {
            return FileModification.Load();
        }
        public static string LoadStr() {
            return FileModification.LoadStr();
        }
        public static void HardSave() {
            FileModification.HardSave();
        }
        public static bool Contains() {
            return FileModification.Contains();
        }
        */
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
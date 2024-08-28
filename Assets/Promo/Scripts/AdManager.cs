using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Advertisements;
//using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public enum ChannelName
    {
        Fridaybox,
        TapGamesFever,
        Petmaster,
        video,
        newXml
    }
   // public bool forreward;
   // [Header("Next Scene To Load")]
   // private int NextSceneToload = 1;
    // private int NextSceneToload;
   // public float timetoload;
    [Header("XML Url")]
    public string AllCommonUrl;
    private bool IsMenuAdOpened;
    public bool IsPortrait;
    public bool IsMenuLoaded = false;
    //public string MenuAdUrl = "";
    //[Header("Portrait")]
    public bool Hint;
    public int count;
    //[HideInInspector]
    public List<Sprite> MgImgList = new List<Sprite>();
    //[HideInInspector]
    public List<string> MgImgLinkList = new List<string>();
    //[HideInInspector]
    public List<string> MgLinkToList = new List<string>();
    //[Header("icon")]
    //	[HideInInspector]
    public List<Sprite> Iconlist = new List<Sprite>();
    //[HideInInspector]
    public List<string> IconLinkList = new List<string>();
    //[HideInInspector]
    public List<string> IconToList = new List<string>();

    //[Header("landscape")]
    //[HideInInspector]
    public List<Sprite> LandList = new List<Sprite>();
    //	[HideInInspector]
    public List<string> LandLinkList = new List<string>();
    //	[HideInInspector]
    public List<string> LandToList = new List<string>();
    public static AdManager instance;
    public bool IconLoaded;
    public ChannelName channelName;

    public string placementId = "bannerPlacement";

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }


    }
    void OnEnable()
    {
        switch (channelName)
        {
            case ChannelName.Fridaybox:
                AllCommonUrl = "https://fridayboxbucket.s3.amazonaws.com/fridaybox.xml";
                break;
            case ChannelName.TapGamesFever:
                AllCommonUrl = "https://tapgamesfever.s3.amazonaws.com/tapgamesfever.xml";
                break;
            case ChannelName.Petmaster:
                AllCommonUrl = "https://petgamesfree.s3.ap-south-1.amazonaws.com/petgamesfree.xml";
                break;
            case ChannelName.video:
                AllCommonUrl = "https://petgamesfree.s3.ap-south-1.amazonaws.com/Video.xml";
                break;
            case ChannelName.newXml:
                AllCommonUrl = "https://hypercausualgamesfree.s3.ap-south-1.amazonaws.com/hypercausualgames/snakegame.xml";
                break;

        }

    }

    // Use this for initialization
    void Start()
    {
 }

    IEnumerator LoadNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

    public IEnumerator OpenMenuScene(float waitTime, bool IsFromXml = false)
    {
        yield return new WaitForSeconds(waitTime);
        if (IsMenuLoaded)
        {
            yield break;
        }
        Debug.Log("-------- CallToOpenMenuScene IsFromXml=" + IsFromXml);
        //	Loading.SetActive (true);
        CancelInvoke("OpenMenuScene");
        IsMenuLoaded = true;
    }
    public bool isWifi_OR_Data_Availble()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork
            || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void showBiGPromot()
    {
        StartCoroutine(MenuAdPage.instance.LoadImg());
    }
    public void showadPromo()
    {
        StartCoroutine(MenuAdPage.instance.ShowAdInGamea());
    }
    [Header("Unity ID")]
    [SerializeField] string UnityGameID = "33675";
    public void ShowAd(string zone = "")
    {

    }
    public bool skipe;


    //[Header("Admob Id")]
    //	public string bannerAdId, interstitialAdId, rewardVideoAdId;
    //public bool  isDebug;
    //[Header("BannerPos")]
    //public  bool isOnTop;
    // 	public static RewardBasedVideoAd rewardBasedVideo;
    // private static BannerView bannerView;
    // private static InterstitialAd interstitial ;
    // public int adscount;
    // void OnGUI()
    // {
    //     if (isDebug)
    //     {
    //         if (GUI.Button(new Rect(20, 0, 100, 60), "Load Full"))
    //         {
    //             //				loadInterstitial ();
    //         }
    //         if (GUI.Button(new Rect(20, 80, 100, 60), "Load Video"))
    //         {
    //             loadRewardVideo();
    //         }
    //         if (GUI.Button(new Rect(20, 160, 100, 60), "Show Banner"))
    //         {
    //             showBannerAd();
    //         }
    //         if (GUI.Button(new Rect(200, 0, 100, 60), "Show Full"))
    //         {
    //             showInterstitial();
    //         }
    //         if (GUI.Button(new Rect(200, 80, 100, 60), "Show Video"))
    //         {
    //             showRewardVideo();
    //         }
    //         if (GUI.Button(new Rect(200, 160, 100, 60), "Hide Banner"))
    //         {
    //             hideBannerAd();
    //         }
    //     }
    // }

    // void onInterstitialEvent(object sender, System.EventArgs args)
    // {
    //     print("OnAdLoaded event received.");
    //     // Handle the ad loaded event.
    // }
    // void onInterstitialCloseEvent(object sender, System.EventArgs args)
    // {
    //     print("OnAdLoaded event received.");
    //     // Handle the ad loaded event.
    // }
    // void onBannerEvent(string eventName, string msg)
    // {

    // }
    // void onadclosed(object sender, System.EventArgs args)
    // {
    //     //		ForLives = false;
    //     Hint = false;
    // }
    // void onRewardedVideoEvent(object sender, Reward args)
    // {
    //     string type = args.Type;
    //     double amount = args.Amount;
    //     print("User rewarded with: " + amount.ToString() + " " + type);
    //     if (Hint == true)
    //     {
    //         Hint = false;
    //         if (SceneManager.GetActiveScene().buildIndex > 10)
    //         {
    //             FindObjectOfType<ButtonCanvas>().HintInstantiate();

    //         }
    //     }
    // }
    // public void showBannerAd()
    // {
    //     if (isOnTop)
    //     {
    //         bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Top);
    //         AdRequest request = new AdRequest.Builder().Build();
    //         // Load the banner with the request.
    //         bannerView.LoadAd(request);
    //     }
    //     else
    //     {
    //         bannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
    //         AdRequest request = new AdRequest.Builder().Build();
    //         // Load the banner with the request.
    //         bannerView.LoadAd(request);
    //     }
    //     // Create an empty ad request.
    // }
    // public void loadInterstitial()
    // {
    //     interstitial = new InterstitialAd(interstitialAdId);
    //     AdRequest request = new AdRequest.Builder().Build();
    //     // Load the interstitial with the request.
    //     interstitial.LoadAd(request);
    //     interstitial.OnAdOpening += onInterstitialEvent;
    //     interstitial.OnAdClosed += onInterstitialCloseEvent;
    // }
    // public void showInterstitial()
    // {
    //     if (interstitial.IsLoaded())
    //     {
    //         interstitial.Show();
    //     }
    //     else
    //     {
    //         loadInterstitial();
    //     }
    // }
    // public void loadRewardVideo()
    // {
    //     rewardBasedVideo = RewardBasedVideoAd.Instance;
    //     AdRequest request = new AdRequest.Builder().Build();
    //     rewardBasedVideo.LoadAd(request, rewardVideoAdId);
    // }
    // public void showRewardVideo()
    // {

    //     if (rewardBasedVideo.IsLoaded())
    //     {
    //         rewardBasedVideo.Show();
    //     }
    //     else
    //     {
    //         loadRewardVideo();
    //     }
    // }
    // public void hideBannerAd()
    // {
    //     bannerView.Hide();
    // }
}
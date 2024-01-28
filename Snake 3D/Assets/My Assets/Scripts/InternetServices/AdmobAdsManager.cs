using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdmobAdsManager : MonoBehaviour
{
    public GButtons GButtonsScript;

    private RewardedAd rewardedAd;

    //Android App Test Id = ca-app-pub-3940256099942544~1458002511

    // All Ad Units
#if UNITY_ANDROID
    private string RewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
#elif UNITY_IPHONE
  private string _adUnitId = "unused";
#else
    private string _adUnitId = "unused";
#endif

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        //Load All Ads
        LoadRewardedAd();

    }

    //************************************************************************************************************Rewarded Ad
    //Load Rewarded Ad
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var RadRequest = new AdRequest();
        RadRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(RewardedAdUnitId, RadRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    //Show Rewarded Ad
    public void ShowRAd()
    {

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log("Player Got Reward");
                GButtonsScript.Revieve();
                LoadRewardedAd();

            });
        }

        else
        {
            Debug.LogError("RewardedAd ad is not ready yet.");

            //Loading Ad Becouse its not ready
            LoadRewardedAd();
        }
    }


    public void ShowRewardedAd()
    {
        ShowRAd();
    }
}
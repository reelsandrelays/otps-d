using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//using GoogleMobileAds.Api;

public class RewardedVideo
{
//    public RewardedVideo(string adUnitID)
//    {
//        AdUnitID = adUnitID;
//        requestCount = 0;

//        LoadVideo();
//    }

//    private int requestCount;
//    private readonly string AdUnitID;

//    public RewardedAd RewardedAd { get; private set; }

//    public void PlayVideo()
//    {
//        AdsManager.RewardedVideo.Remove(this);
//        RewardedAd.Show();
//    }

//    private void LoadVideo()
//    {
//        requestCount++;

//        RewardedAd = new RewardedAd(AdUnitID);
//        RewardedAd.OnAdOpening += OnAdOpening;
//        RewardedAd.OnAdFailedToLoad += OnAdFailedToLoad;
//        RewardedAd.OnAdClosed += OnAdClosed;
//        RewardedAd.OnUserEarnedReward += OnUserEarnedReward;
//        RewardedAd.LoadAd(new AdRequest.Builder().Build());
//    }

//    private void OnAdOpening(object sender, EventArgs eventArgs)
//    {
////#if UNITY_IOS
////        LifeManager.Pause();
////        AudioManager.Instance.MusicController.Pause(1);
////#endif
//    }

////#if UNITY_IOS
////    private void IOS_TrySoundRevival()
////    {
////        AudioListener.volume = 1.0f;
////        if (GameInstance.BGMOn) AudioManager.Instance.MixerControl.SetChannelVolume("MusicMaster", 0f);
////        if (GameInstance.SFXOn) AudioManager.Instance.MixerControl.SetChannelVolume("SFXMaster", 0f);
////    }
////#endif

//    private void OnAdClosed(object sender, EventArgs eventArgs)
//    {
////#if UNITY_IOS
////        IOS_TrySoundRevival();
////        LifeManager.Resume();
////        if (GameInstance.BGMOn && string.Equals(GameInstance.CurrentSceneName, "Main")) AudioManager.Instance.MusicController.Resume(1);
////#endif

//        // 공통
//        //LoadingFilm.Instance.Off();
//    }

//    private void OnAdFailedToLoad(object sender, AdErrorEventArgs adErrorEventArgs)
//    {
//        Debug.LogError("Video loading FAILED: " + adErrorEventArgs.Message);

//        if (requestCount >= 10) AdsManager.RewardedVideo.Remove(this);
//        else LoadVideo();
//    }

//    private void OnUserEarnedReward(object sender, Reward reward) { AdsManager.GetReward?.Invoke(); }
}

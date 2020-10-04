using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Abs
{
    public sealed class UnityAbs : MonoBehaviour
    {
        private const string GOOGLE_PLAY_ID = "3848203";
        private const string APP_STORE_ID = "3848202";
        
        private const string PLACEMENT_ID_BANNER = "banner";
        private const string PLACEMENT_ID_REWARDED_VIDEO = "rewardedVideo";
        private const string PLACEMENT_ID_VIDEO = "video";

        [SerializeField] private Button _bannerButton;
        [SerializeField] private Button _videButton;
        [SerializeField] private Button _rewardVideButton;
        [SerializeField] private BannerPosition _bannerPosition;
        private IAbsBanner _absBanner;
        private IAbsVideo _absVideo;
        private IAbsRewardedVideo _absRewardedVideo;
        private IUnityAdsListener _unityAdsListener;

        private void Awake()
        {
            string gameId = GOOGLE_PLAY_ID;

            #if UNITY_ANDROID
        gameId = GOOGLE_PLAY_ID;
            #elif UNITY_IOS
        gameId = APP_STORE_ID;
            #endif

            _absBanner = new AbsBanner(PLACEMENT_ID_BANNER);
            _absVideo = new AbsVideo(PLACEMENT_ID_VIDEO);
            _absRewardedVideo = new AbsRewardedVideo(PLACEMENT_ID_REWARDED_VIDEO);
            _unityAdsListener = new UnityAdsListener(PLACEMENT_ID_REWARDED_VIDEO);

            Advertisement.Initialize(gameId);
        }

        private void OnEnable()
        {
            Advertisement.AddListener(_unityAdsListener);
            _bannerButton.onClick.AddListener(ShowAbsBanner);
            _videButton.onClick.AddListener(ShowAbsVideo);
            _rewardVideButton.onClick.AddListener(ShowAbsRewardedVideo);
        }

        private void OnDisable()
        {
            Advertisement.RemoveListener(_unityAdsListener);
            _bannerButton.onClick.RemoveListener(ShowAbsBanner);
            _videButton.onClick.RemoveListener(ShowAbsVideo);
            _rewardVideButton.onClick.RemoveListener(ShowAbsRewardedVideo);
        }

        private void ShowAbsVideo()
        {
            if (_absVideo.IsReady())
            {
                _absVideo.Show();
            }
        }

        private void ShowAbsRewardedVideo()
        {
            if (_absRewardedVideo.IsReady())
            {
                _absRewardedVideo.Show();
            }
        }

        private void ShowAbsBanner()
        {
            StartCoroutine(ShowAbsBannerReady());
        }

        private IEnumerator ShowAbsBannerReady()
        {
            while (!_absBanner.IsReady())
            {
                yield return new WaitForSeconds(1.0f);
            }
            _absBanner.Show(_bannerPosition);
        }
    }
}

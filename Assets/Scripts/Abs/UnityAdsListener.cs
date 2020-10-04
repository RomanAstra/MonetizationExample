using UnityEngine;
using UnityEngine.Advertisements;

namespace Abs
{
    internal sealed class UnityAdsListener : IUnityAdsListener
    {
        private string _myPlacementId;
        private bool _adsAreReady;

        public bool AdsAreReady => _adsAreReady;

        public UnityAdsListener(string myPlacementId)
        {
            _myPlacementId = myPlacementId;
        }

        public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) 
        {
            // Define conditional logic for each ad completion status:
            switch(showResult)
            {
                case ShowResult.Finished:
                    Debug.LogWarning ("Finished");
                    break;

                case ShowResult.Skipped:
                    Debug.LogWarning ("Skipped");
                    break;

                case ShowResult.Failed:
                    Debug.LogWarning ("Failed");
                    break;
            }
        }

        public void OnUnityAdsReady (string placementId) 
        {
            if (placementId != _myPlacementId) return;

            _adsAreReady = true;
        }

        public void OnUnityAdsDidError (string message) 
        {
            // Log the error.
        }

        public void OnUnityAdsDidStart (string placementId) 
        {
            // Optional actions to take when the end-users triggers an ad.
        } 
    }
}

using UnityEngine.Advertisements;

namespace Abs
{
    internal class AbsRewardedVideo : IAbsRewardedVideo
    {
        private readonly string _placementID;

        public AbsRewardedVideo(string placementID)
        {
            _placementID = placementID;
        }

        public bool IsReady() => Advertisement.IsReady(_placementID);

        public void Show()
        {
            ShowOptions options = new ShowOptions();
            Advertisement.Show(_placementID, options);
        }
    }
}

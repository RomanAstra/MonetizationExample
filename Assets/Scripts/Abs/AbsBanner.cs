using UnityEngine.Advertisements;

namespace Abs
{
    internal class AbsBanner : IAbsBanner
    {
        private readonly string _placementID;

        public AbsBanner(string placementID)
        {
            _placementID = placementID;
        }

        public bool IsReady() => Advertisement.IsReady(_placementID);

        public void Show(BannerPosition bannerPosition)
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(_placementID);
        }
    }
}

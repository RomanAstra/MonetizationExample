using UnityEngine.Advertisements;

namespace Abs
{
    internal class AbsVideo : IAbsVideo
    {
        private readonly string _placementID;

        public AbsVideo(string placementID)
        {
            _placementID = placementID;
        }

        public bool IsReady() => Advertisement.IsReady(_placementID);

        public void Show()
        {
            Advertisement.Show(_placementID);
        }
    }
}

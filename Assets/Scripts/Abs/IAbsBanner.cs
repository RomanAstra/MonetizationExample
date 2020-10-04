using UnityEngine.Advertisements;

namespace Abs
{
    public interface IAbsBanner
    {
        bool IsReady();
        void Show(BannerPosition bannerPosition);
    }
}

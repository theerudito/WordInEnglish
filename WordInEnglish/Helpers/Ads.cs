using MarcTron.Plugin;

namespace WordInEnglish.Helpers
{
    public static class Ads
    {
        public static void ShowIntertiscal()
        {
            var idIntersticial = "ca-app-pub-7633493507240683/8231562165";

            CrossMTAdmob.Current.LoadInterstitial(idIntersticial);
        }

        public static bool IsIntertiscalLoaded()
        {
            return CrossMTAdmob.Current.IsInterstitialLoaded() ? true : false;
        }

        public static void ShowRewardedVideo()
        {
            var idVideo = "ca-app-pub-7633493507240683/9925478281";

            CrossMTAdmob.Current.LoadRewardedVideo(idVideo);
        }

        public static bool IsVideoLoaded()
        {
            return CrossMTAdmob.Current.IsRewardedVideoLoaded() ? true : false;
        }

    }
}

using Xamarin.Essentials;

namespace WordInEnglish.Helpers
{
    public static class ValidationInternet
    {
        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
    }
}
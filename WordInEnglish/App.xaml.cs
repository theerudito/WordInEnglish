using Microsoft.EntityFrameworkCore;
using Plugin.FirebasePushNotification;
using WordInEnglish.Application_Context;
using WordInEnglish.Model;
using WordInEnglish.View;
using Xamarin.Forms;

namespace WordInEnglish
{
    public partial class App : Application
    {
        public App()
        {
            var _dbContext = new Application_ContextDB();
            var _data = new InformationData();
            _dbContext.Database.Migrate();

            var language = GetDeviceLanguage();

            Xamarin.Essentials.Preferences.Set("language", language);

            var searhEN = _dbContext.WordsEN.Find(1);

            if (searhEN == null)
            {
                _data.WORDEN();
            }

            var searhES = _dbContext.WordsES.Find(1);

            if (searhES == null)
            {
                _data.WORDES();
            }

            InitializeComponent();

            MainPage = new NavigationPage(new Home());

            CrossFirebasePushNotification.Current.Subscribe("all");
            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }

        public string GetDeviceLanguage()
        {
            string language = "";

            if (Device.RuntimePlatform == Device.Android)
            {
                var androidLocale = Java.Util.Locale.Default;
                language = androidLocale.Language;
            }

            // hacer una alerta

            System.Diagnostics.Debug.WriteLine("idioma: " + language);

            return language;
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TOKEN ERUDITO: {e.Token}");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
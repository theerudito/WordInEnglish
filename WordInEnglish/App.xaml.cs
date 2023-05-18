using Microsoft.EntityFrameworkCore;
using Plugin.FirebasePushNotification;
using Plugin.Multilingual;
using System;
using WordInEnglish.Application_Context;
using WordInEnglish.Helpers;
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

            LocalStorange.SetLocalStorange("language", getLanguage());

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

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TOKEN ERUDITO: {e.Token}");
        }

        private string getLanguage()
        {
            var currentLanguage = CrossMultilingual.Current.CurrentCultureInfo;

            Console.WriteLine("Idioma es " + currentLanguage.ToString());
            if (currentLanguage.ToString() == "en-US")
            {
                return "EN";
            }
            else if (currentLanguage.ToString() == "es-ES")
            {
                return "ES";
            }
            else
            {
                return "EN";
            }
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
using MarcTron.Plugin;
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
            Ads.ShowRewardedVideo();

            getLanguage();

            var _dbContext = new Application_ContextDB();
            var _data = new InformationData();
            _dbContext.Database.Migrate();


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



            CrossMTAdmob.Current.OnRewardedVideoAdLoaded += (s, args) =>
            {
                CrossMTAdmob.Current.ShowRewardedVideo();
            };

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");

                Xamarin.Essentials.Preferences.Set("token", p.Token);
            };
            // Push message received event
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };
            //Push message received event
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

            };

            MainPage = new NavigationPage(new Home());

        }

        public void getLanguage()
        {
            var currentLanguage = CrossMultilingual.Current.CurrentCultureInfo;

            Console.WriteLine("Idioma es " + currentLanguage.Name);

            if (currentLanguage.Name == "en-US")
            {
                LocalStorange.SetLocalStorange("language", "EN");
            }
            else if (currentLanguage.Name == "es-ES")
            {
                LocalStorange.SetLocalStorange("language", "ES");
            }
            else
            {
                LocalStorange.SetLocalStorange("language", "EN");
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
using Microsoft.EntityFrameworkCore;
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
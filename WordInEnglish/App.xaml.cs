using Microsoft.EntityFrameworkCore;
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
            var _data = new Data();

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

            MainPage = new Home();
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
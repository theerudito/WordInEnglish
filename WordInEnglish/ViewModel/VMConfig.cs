using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglish.Application_Context;
using WordInEnglish.Model;
using Xamarin.Forms;

namespace WordInEnglish.ViewModel
{
    public class VMConfig : BaseVM
    {
        private Application_ContextDB _Context = new Application_ContextDB();
        public VMConfig(INavigation navigation)
        {
            Navigation = navigation;
        }

        #region Property
        private string _WordEnglish;
        private string _WordSpanish;
        #endregion


        #region Objects
        public string WordEnglish
        {
            get { return _WordEnglish; }
            set { _WordEnglish = value; OnPropertyChanged(); }
        }
        public string WordSpanish
        {
            get { return _WordSpanish; }
            set { _WordSpanish = value; OnPropertyChanged(); }
        }

        #endregion


        #region Methods
        public async Task GoConfig()
        {
            await Navigation.PopAsync();
        }

        public async Task SaveNewWord()
        {
            var neWordEN = new WordEN
            {
                MyWord = WordEnglish.ToUpper().Trim()
            };

            var neWordES = new WordES
            {
                MyWord = WordSpanish.ToUpper().Trim()
            };

            await _Context.AddAsync(neWordEN);
            await _Context.AddAsync(neWordES);
            await _Context.SaveChangesAsync();

            await DisplayAlert("WordInEnglish", "Word saved successfully", "Ok");
            Clean();
        }

        public void Clean()
        {
            WordEnglish = string.Empty;
            WordSpanish = string.Empty;
        }
        #endregion

        #region Commands
        public ICommand btnBack => new Command(async () => await GoConfig());
        public ICommand btnSaveNewWord => new Command(async () => await SaveNewWord());
        #endregion
    }







}
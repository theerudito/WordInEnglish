using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglish.Application_Context;
using WordInEnglish.Model;
using Xamarin.Forms;

namespace WordInEnglish.ViewModel
{
    public class VMConfig : BaseVM
    {
        private Application_ContextDB _context = new Application_ContextDB();

        public VMConfig(INavigation navigation)
        {
            Navigation = navigation;
        }

        #region Property

        private string _WordEnglish;
        private string _WordSpanish;

        #endregion Property

        #region Objects

        public string WordEnglish
        {
            get => _WordEnglish;
            set { _WordEnglish = value; OnPropertyChanged(); }
        }

        public string WordSpanish
        {
            get { return _WordSpanish; }
            set { _WordSpanish = value; OnPropertyChanged(); }
        }

        #endregion Objects

        #region Methods

        public async Task GoConfig()
        {
            await Navigation.PopAsync();
        }

        public async Task SaveNewWord()
        {
            string wordEnglish = WordEnglish.ToUpper().Trim();
            string wordSpanish = WordSpanish.ToUpper().Trim();

            var searchEN = _context.WordsEN.Where(x => x.MyWord.Equals(wordEnglish.Length)).FirstOrDefault();
            var searchES = _context.WordsES.Where(x => x.MyWord.Equals(wordSpanish.Length)).FirstOrDefault();

            if (searchEN != null || searchES != null)
            {
                await DisplayAlert("Info", "Word already exists", "Ok");
            }
            else
            {
                if (ValidateFields() == true)
                {
                    await _context.AddAsync(new WordEN { MyWord = WordEnglish.ToUpper().Trim() });
                    await _context.AddAsync(new WordES { MyWord = WordSpanish.ToUpper().Trim() });
                    await _context.SaveChangesAsync();

                    await DisplayAlert("Info", "Word saved successfully", "Ok");
                    CleanFields();
                }
            }
        }

        public void CleanFields()
        {
            WordEnglish = string.Empty;
            WordSpanish = string.Empty;
        }

        public bool ValidateFields()
        {
            if (string.IsNullOrEmpty(WordEnglish))
            {
                DisplayAlert("WordInEnglish", "Please enter a word in English", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(WordSpanish))
            {
                DisplayAlert("WordInEnglish", "Please enter a word in Spanish", "Ok");
                return false;
            }
            return true;
        }

        #endregion Methods

        #region Commands

        public ICommand btnBack => new Command(async () => await GoConfig());
        public ICommand btnSaveNewWord => new Command(async () => await SaveNewWord());

        #endregion Commands
    }
}
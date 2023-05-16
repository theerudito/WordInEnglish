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
            if (getLocalStorange() == "EN")
            {
                LanguageConfig();
            }
            else
            {
                LanguageConfig();
            }
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

        #region Language;

        private string _language;
        private string _textEN;
        private string _textES;
        private string _placeholderEN;
        private string _placeholderES;
        private string _btnTextEN;

        public string Language
        {
            get { return _language; }
            set { _language = value; OnPropertyChanged(); }
        }

        public string TextInforEN
        {
            get { return _textEN; }
            set { _textEN = value; OnPropertyChanged(); }
        }

        public string TextInforES
        {
            get { return _textES; }
            set { _textES = value; OnPropertyChanged(); }
        }

        public string PlaceholderTextEN
        {
            get { return _placeholderEN; }
            set { _placeholderEN = value; OnPropertyChanged(); }
        }

        public string PlaceholderTextES
        {
            get { return _placeholderES; }
            set { _placeholderES = value; OnPropertyChanged(); }
        }

        public string BtnText
        {
            get { return _btnTextEN; }
            set { _btnTextEN = value; OnPropertyChanged(); }
        }

        #endregion Language;

        #endregion Objects

        #region Methods

        public void LanguageConfig()
        {
            getLocalStorange();
            if (Language == "EN")
            {
                TextInforEN = MyLanguages._TextOneEN;
                TextInforES = MyLanguages._TextTwoEN;
                PlaceholderTextEN = MyLanguages._PlaceholderOneEN;
                PlaceholderTextES = MyLanguages._PlaceholderOneEN;
                BtnText = MyLanguages._SaveWORDEN;
            }
            else
            {
                TextInforEN = MyLanguages._TextOneES;
                TextInforES = MyLanguages._TextTwoES;
                PlaceholderTextEN = MyLanguages._PlaceholderTwoES;
                PlaceholderTextES = MyLanguages._PlaceholderTwoES;
                BtnText = MyLanguages._SaveWORDES;
            }
        }

        public async Task GoConfig()
        {
            await Navigation.PopAsync();
        }

        public async Task SaveNewWord()
        {
            if (ValidateFields() == true)
            {
                string wordEnglish = WordEnglish.ToUpper().Trim();
                string wordSpanish = WordSpanish.ToUpper().Trim();

                var wordEN = _context.WordsEN.Where(x => x.MyWord == wordEnglish).FirstOrDefault();
                var wordES = _context.WordsES.Where(x => x.MyWord == wordSpanish).FirstOrDefault();

                if (wordEN != null && wordES != null)
                {
                    var wordEN_ID = _context.WordsEN.Where(x => x.IdEN == wordEN.IdEN).FirstOrDefault();
                    var wordES_ID = _context.WordsES.Where(x => x.IdES == wordES.IdES).FirstOrDefault();

                    if (wordEN_ID != null && wordES_ID != null)
                    {
                        await AlertExistWord();
                    }
                    else
                    {
                        await newWord();
                    }
                }
                else
                {
                    await newWord();
                }
            }
        }

        public async Task newWord()
        {
            if (ValidateFields() == true)
            {
                await _context.AddAsync(new WordEN { MyWord = WordEnglish.ToUpper().Trim() });
                await _context.AddAsync(new WordES { MyWord = WordSpanish.ToUpper().Trim() });
                await _context.SaveChangesAsync();
                await AlertSaveSuccessfully();
                CleanFields();
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
                AlertValidationFieldOne();
                return false;
            }
            if (string.IsNullOrEmpty(WordSpanish))
            {
                AlertValidationFieldTwo();
                return false;
            }
            return true;
        }

        public string getLocalStorange()
        {
            var language = Xamarin.Essentials.Preferences.Get("language", "EN");
            Language = language;
            return language;
        }

        public async Task AlertExistWord()
        {
            if (Language == "EN")
            {
                await DisplayAlert("Info", "The word already exists", "Ok");
            }
            else
            {
                await DisplayAlert("Info", "La palabra ya existe", "Ok");
            }
        }

        public async Task AlertSaveSuccessfully()
        {
            if (Language == "EN")
            {
                await DisplayAlert("Info", "Word saved successfully", "Ok");
            }
            else
            {
                await DisplayAlert("Info", "Palabra guardada exitosamente", "Ok");
            }
        }

        public void AlertValidationFieldOne()
        {
            if (Language == "EN")
            {
                DisplayAlert("WordInEnglish", "Please enter a word in English", "Ok");
            }
            else
            {
                DisplayAlert("WordInEnglish", "Por favor ingrese una palabra en Ingles", "Ok");
            }
        }

        public void AlertValidationFieldTwo()
        {
            if (Language == "EN")
            {
                DisplayAlert("WordInEnglish", "Please enter a word in Spanish", "Ok");
            }
            else
            {
                DisplayAlert("WordInEnglish", "Por favor ingrese una palabra en Español", "Ok");
            }
        }

        #endregion Methods

        #region Commands

        public ICommand btnBack => new Command(async () => await GoConfig());
        public ICommand btnSaveNewWord => new Command(async () => await SaveNewWord());

        #endregion Commands
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglish.Application_Context;
using Xamarin.Forms;

namespace WordInEnglish.ViewModel
{
    public class VM_Home : BaseVM
    {
        private Application_ContextDB _Context = new Application_ContextDB();

        #region CONSTRUCTOR

        public VM_Home(INavigation navigation)
        {
            Navigation = navigation;

            LabelPoints = "Points:";
            ScoreColor = "GreenYellow";

            GenerateWord();

            Score();
        }

        #endregion CONSTRUCTOR

        #region VARIABLE

        private string _labelPoints;
        private string _colorScore;
        private string _labelTextWord;
        private string _labeltextResult;
        private bool _reponseCorrectText;
        private string _inputTextEntry;
        private int IdWord;

        private int _labelCounter;
        private static int _points = 0;
        public int _pointResponseConrrect = _points + 10;
        public int _pointResponseIncorrect = _points - 10;
        private int _trying = 0;

        #endregion VARIABLE

        #region OBJECTS

        public string LabelPoints
        {
            get { return _labelPoints; }
            set
            {
                _labelPoints = value;
                OnPropertyChanged(nameof(LabelPoints));
            }
        }

        public int LabelCounter
        {
            get { return _labelCounter; }
            set
            {
                SetValue(ref _labelCounter, value);
                OnPropertyChanged(nameof(LabelCounter));
            }
        }

        public string LabelWord
        {
            get { return _labelTextWord; }
            set
            {
                _labelTextWord = value;
                OnPropertyChanged(nameof(LabelWord));
            }
        }

        public string EntryWord
        {
            get { return _inputTextEntry; }
            set
            {
                SetValue(ref _inputTextEntry, value);
                OnPropertyChanged(nameof(EntryWord));
            }
        }

        public string ScoreColor
        {
            get { return _colorScore; }
            set
            {
                SetValue(ref _colorScore, value);
                OnPropertyChanged(nameof(ScoreColor));
            }
        }

        public int Trying
        {
            get { return _trying; }
            set
            {
                SetValue(ref _trying, value);
                OnPropertyChanged(nameof(Trying));
            }
        }

        public string WordCorrect
        {
            get { return _labeltextResult; }
            set
            {
                SetValue(ref _colorScore, value);
                OnPropertyChanged(nameof(WordCorrect));
            }
        }

        #endregion OBJECTS

        #region METHODS

        public async Task CheckWord()
        {
            var query = await _Context.WordsES.FindAsync(IdWord);

            if (query != null)
            {
                if (query.MyWord == EntryWord)
                {
                    await DisplayAlert("info", "Correcto", "ok");

                    _reponseCorrectText = true;

                    WordCorrect = query.MyWord;

                    Score();

                    await GenerateWord();
                }
                else
                {
                    await DisplayAlert("error", "Incorrecto", "ok");

                    _reponseCorrectText = false;

                    Score();

                    Trying++;

                    if (Trying == 3)
                    {
                        Score();

                        WordCorrect = query.MyWord;

                        Trying = 0;
                    }

                    if (Trying == 0)
                    {
                        await GenerateWord();
                    }
                }
            }
        }

        public void Score()
        {
            LabelCounter = _reponseCorrectText == true ? _pointResponseConrrect : _pointResponseIncorrect;
        }

        public async Task GenerateWord()
        {
            try
            {
                _reponseCorrectText = true;

                WordCorrect = "?";

                var quantityOnTable = _Context.WordsEN.ToListAsync().Result.Count;

                Random random = new Random();

                int numRandom = random.Next(1, quantityOnTable);

                var generateWord = await _Context.WordsEN.Where(word => word.IdEN == numRandom).FirstOrDefaultAsync();

                IdWord = generateWord.IdEN;

                LabelWord = generateWord.MyWord;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion METHODS

        #region COMMANDS

        public ICommand btnCheck => new Command(async () => await CheckWord());

        #endregion COMMANDS
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            ScoreColor = ColorCorrect();
            ColorInitial = "Orange";

            GenerateWord();

            SelectWord();

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
        private int _IdWord;

        private string _wordOne;
        private string _wordTwo;
        private string _wordThree;

        private string _colorInitial;

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
                OnPropertyChanged();
            }
        }

        public int LabelCounter
        {
            get { return _labelCounter; }
            set
            {
                _labelCounter = value;
                OnPropertyChanged();
            }
        }

        public string LabelWord
        {
            get { return _labelTextWord; }
            set
            {
                _labelTextWord = value;
                OnPropertyChanged();
            }
        }

        public string EntryWord
        {
            get { return _inputTextEntry; }
            set
            {
                _inputTextEntry = value;
                OnPropertyChanged();
            }
        }

        public string ScoreColor
        {
            get { return _colorScore; }
            set
            {
                _colorScore = value;
                OnPropertyChanged();
            }
        }

        public int Trying
        {
            get { return _trying; }
            set
            {
                _trying = value;
                OnPropertyChanged();
            }
        }

        public string WordCorrect
        {
            get { return _labeltextResult; }
            set
            {
                _colorScore = value;
                OnPropertyChanged();
            }
        }

        public int IdWord
        {
            get { return _IdWord; }
            set
            {
                _IdWord = value;
                OnPropertyChanged();
            }
        }

        public string WordOne
        {
            get { return _wordOne; }
            set
            {
                _wordOne = value;
                OnPropertyChanged();
            }
        }

        public string WordTwo
        {
            get { return _wordTwo; }
            set
            {
                _wordTwo = value;
                OnPropertyChanged();
            }
        }

        public string WordThree
        {
            get { return _wordThree; }
            set
            {
                _wordThree = value;
                OnPropertyChanged();
            }
        }

        public string ColorInitial
        {
            get { return _colorInitial; }
            set
            {
                SetValue(ref _colorInitial, value);
                OnpropertyChanged();
            }
        }

        private int[] IdWordData = { 1, 2, 3 };

        #endregion OBJECTS

        #region METHODS

        public async Task CheckWordEntry()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            if (Word != null)
            {
                if (EntryWord.ToUpper() == Word.MyWord.ToUpper())
                {
                    await DisplayAlert("Correct", Word.MyWord, "OK");
                    ScoreColor = ColorCorrect();
                    ColorInitial = ColorCorrect();
                    Score();
                    await GenerateWord();
                    await SelectWord();
                    EntryWord = "";
                }
                else
                {
                    ScoreColor = ColorError();
                    Score();
                    await DisplayAlert("Correct", "Error", "OK");
                    ColorInitial = ColorError();
                    ScoreColor = ColorError();
                }
            }
            else
            {
                await DisplayAlert("Error", "Palabra No Registrada", "OK");
            }
        }

        public async Task CheckWordOne()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            if (IdWordData[0] == Word.IdES)
            {
                ColorInitial = ColorCorrect();
            }
            else
            {
                ScoreColor = ColorError();
            }
        }

        public async Task CheckWordTwo()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();
            if (IdWordData[1] == Word.IdES)
            {
                ColorInitial = ColorCorrect();
            }
            else
            {
                ScoreColor = ColorError();
            }
        }

        public async Task CheckWordThree()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            if (IdWordData[2] == Word.IdES)
            {
                ColorInitial = ColorCorrect();
            }
            else
            {
                ScoreColor = ColorError();
            }
        }

        public string ColorCorrect()
        {
            return "GreenYellow";
        }

        public string ColorError()
        {
            return "Red";
        }

        public async Task SelectWord()
        {
            List<int> numsRandom = numAletory(3);

            var generateWordOne = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();
            WordOne = generateWordOne.MyWord.ToUpper();
            IdWordData[0] = generateWordOne.IdES;

            var generateWordTwo = await _Context.WordsES.Where(word => word.IdES == numsRandom[1]).FirstOrDefaultAsync();
            WordTwo = generateWordTwo.MyWord.ToUpper();
            IdWordData[1] = generateWordTwo.IdES;

            var generateWordThree = await _Context.WordsES.Where(word => word.IdES == numsRandom[2]).FirstOrDefaultAsync();
            WordThree = generateWordThree.MyWord.ToUpper();
            IdWordData[2] = generateWordThree.IdES;

            var random = new Random();

            var num = random.Next(1, 4);

            //if (num == 1)
            //{
            //    WordOne = generateWordOne.MyWord.ToUpper();
            //    WordTwo = generateWordTwo.MyWord.ToUpper();
            //    WordThree = generateWordThree.MyWord.ToUpper();

            //    IdWordData[0] = generateWordOne.IdES;
            //}
            //else if (num == 2)
            //{
            //    WordOne = generateWordTwo.MyWord.ToUpper();
            //    WordTwo = generateWordOne.MyWord.ToUpper();
            //    WordThree = generateWordThree.MyWord.ToUpper();

            //    IdWordData[1] = generateWordTwo.IdES;
            //}
            //else if (num == 3)
            //{
            //    WordOne = generateWordThree.MyWord.ToUpper();
            //    WordTwo = generateWordTwo.MyWord.ToUpper();
            //    WordThree = generateWordOne.MyWord.ToUpper();

            //    IdWordData[2] = generateWordThree.IdES;
            //}
        }

        public List<int> numAletory(int count)
        {
            var quantityOnTable = _Context.WordsEN.ToListAsync().Result.Count;

            var list = new List<int>();

            var random = new Random();

            while (list.Count < count)
            {
                var num = random.Next(1, quantityOnTable);
                if (!list.Contains(num))
                {
                    list.Add(num);
                }
            }
            return list;
        }

        public void Score()
        {
            LabelCounter = _reponseCorrectText == true ? _pointResponseConrrect : _pointResponseIncorrect;
        }

        public async Task GenerateWord()
        {
            try
            {
                var quantityOnTable = _Context.WordsEN.ToListAsync().Result.Count;

                var random = new Random();

                var numsRandom = random.Next(1, quantityOnTable);

                _reponseCorrectText = true;

                var generateWord = await _Context.WordsEN.Where(word => word.IdEN == numsRandom).FirstOrDefaultAsync();

                IdWord = generateWord.IdEN;

                LabelWord = generateWord.MyWord.ToUpper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                await SelectWord();
                await GenerateWord();
            }
        }

        #endregion METHODS

        #region COMMANDS

        public ICommand btnCheck => new Command(async () => await CheckWordEntry());

        public ICommand btnCheckWordOne => new Command(async () => await CheckWordOne());
        public ICommand btnCheckWordTwo => new Command(async () => await CheckWordTwo());
        public ICommand btnCheckWordThree => new Command(async () => await CheckWordThree());

        #endregion COMMANDS
    }
}
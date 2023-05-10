using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglish.Application_Context;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WordInEnglish.ViewModel
{
    public class VMHome : BaseVM
    {
        private Application_ContextDB _Context = new Application_ContextDB();

        #region Constructor

        public VMHome(INavigation navigation)
        {
            Navigation = navigation;

            LabelScore = 0;

            ScoreColor = ColorCorrect();

            InitialColor();

            GenerateWord();

            SelectWord();

            Score();

            ImageLanguage = ImageSource.FromFile("flag_EN.png");
        }

        #endregion Constructor

        #region Properties

        private int _labelScore;
        private Color _colorScore;
        private string _labelTextWord;
        private string _labeltextResult;
        private string _inputTextEntry;
        private int _IdWord;

        private string _wordOne;
        private string _wordTwo;
        private string _wordThree;

        private Color _colorFrameOne;
        private Color _colorFrameTwo;
        private Color _colorFrameThree;

        private ImageSource _ImageSource;

        private int _trying = 0;

        #endregion Properties

        #region Getters/Setters

        public int LabelScore
        {
            get { return _labelScore; }
            set
            {
                _labelScore = value;
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

        public Color ScoreColor
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
                _labeltextResult = value;
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

        public Color ColorFrameOne
        {
            get => _colorFrameOne;
            set => SetValue(ref _colorFrameOne, value);
        }

        public Color ColorFrameTwo
        {
            get => _colorFrameTwo;
            set => SetValue(ref _colorFrameTwo, value);
        }

        public Color ColorFrameThree
        {
            get => _colorFrameThree;
            set => SetValue(ref _colorFrameThree, value);
        }

        private int[] IdWordData = { 1, 2, 3 };

        public ImageSource ImageLanguage
        {
            get => _ImageSource;
            set => SetValue(ref _ImageSource, value);
        }

        #endregion Getters/Setters

        #region Methods

        public async Task GenerateWord()
        {
            try
            {
                var quantityOnTable = _Context.WordsEN.ToListAsync().Result.Count;

                var random = new Random();

                var numsRandom = random.Next(1, quantityOnTable);

                var generateWord = await _Context.WordsEN.Where(word => word.IdEN == numsRandom).FirstOrDefaultAsync();

                IdWord = generateWord.IdEN;

                LabelWord = generateWord.MyWord.ToUpper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        public async Task CheckWordEntry()
        {
            if (ValidationEntry() == true)
            {
                var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

                if (Word != null)
                {
                    if (EntryWord.ToUpper() == Word.MyWord.ToUpper())
                    {
                        ScoreColor = ColorCorrect();
                        LabelScore++;
                        SoundCorrect();
                        await Task.Delay(2000);
                        await GenerateWord();
                        await SelectWord();
                        InitialColor();
                        WordCorrect = "";
                        EntryWord = "";
                    }
                    else
                    {
                        SoundInCorrect();
                        await DisplayAlert("Correct", "Intenta Otra Vez", "OK");
                        ScoreColor = ColorError();

                        if (LabelScore > 0)
                        {
                            LabelScore--;
                        }
                        else
                        {
                            LabelScore = 0;
                            await GenerateWord();
                            await SelectWord();
                        }

                        Trying++;
                        if (Trying == 3)
                        {
                            WordCorrect = Word.MyWord.ToUpper();
                            await Task.Delay(2000);
                            await GenerateWord();
                            await SelectWord();
                            Trying = 0;
                            WordCorrect = "";
                            EntryWord = "";
                        }
                    }
                }
                else
                {
                    SoundInCorrect();
                    await DisplayAlert("Error", "Palabra No Registrada", "OK");
                }
            }
        }

        public async Task CheckWordOne()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            if (IdWordData[0] == Word.IdES)
            {
                ColorFrameOne = ColorCorrect();
                ScoreColor = ColorCorrect();
                SoundCorrect();

                LabelScore++;
                await Task.Delay(2000);
                await GenerateWord();
                await SelectWord();
                InitialColor();
            }
            else
            {
                ColorFrameOne = ColorError();
                ScoreColor = ColorError();
                SoundInCorrect();

                if (LabelScore > 0)
                {
                    LabelScore--;
                }
                else
                {
                    LabelScore = 0;
                    await GenerateWord();
                    await SelectWord();
                }
            }
        }

        public async Task CheckWordTwo()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();
            if (IdWordData[1] == Word.IdES)
            {
                ColorFrameTwo = ColorCorrect();
                ScoreColor = ColorCorrect();
                SoundCorrect();

                LabelScore++;
                await Task.Delay(2000);
                await GenerateWord();
                await SelectWord();
                InitialColor();
            }
            else
            {
                ColorFrameTwo = ColorError();
                ScoreColor = ColorError();
                SoundInCorrect();

                if (LabelScore > 0)
                {
                    LabelScore--;
                }
                else
                {
                    LabelScore = 0;
                    await GenerateWord();
                    await SelectWord();
                }
            }
        }

        public async Task CheckWordThree()
        {
            var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            if (IdWordData[2] == Word.IdES)
            {
                ColorFrameThree = ColorCorrect();
                ScoreColor = ColorCorrect();
                SoundCorrect();

                LabelScore++;
                await Task.Delay(2000);
                await GenerateWord();
                await SelectWord();
                InitialColor();
            }
            else
            {
                ColorFrameThree = ColorError();
                ScoreColor = ColorError();
                SoundInCorrect();

                if (LabelScore > 0)
                {
                    LabelScore--;
                }
                else
                {
                    LabelScore = 0;
                    await GenerateWord();
                    await SelectWord();
                }
            }
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
            LabelScore = 10;
        }

        public Color ColorCorrect()
        {
            return Color.GreenYellow;
        }

        public Color ColorError()
        {
            return Color.Red;
        }

        public void InitialColor()
        {
            ColorFrameOne = Color.Yellow;
            ColorFrameTwo = Color.PeachPuff;
            ColorFrameThree = Color.Aqua;
        }

        public void SoundCorrect()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("WordInEnglish.Sound.correct.mp3");
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
        }

        public void SoundInCorrect()
        {
            // VibrateDevice();
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("WordInEnglish.Sound.error.mp3");
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
        }

        public bool ValidationEntry()
        {
            if (string.IsNullOrEmpty(EntryWord))
            {
                DisplayAlert("Error", "Ingrese una palabra", "OK");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ChangeLanguage()
        {
            var imageES = ImageSource.FromFile("flag_ES.png");
            var imageEN = ImageSource.FromFile("flag_EN.png");

            ImageLanguage = ImageLanguage == imageES ? imageEN : imageES;
        }

        public void VibrateDevice()
        {
            try
            {
                Vibration.Vibrate();

                // Or use specified time
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        #endregion Methods

        #region Commands

        public ICommand btnChangeLanguage => new Command(ChangeLanguage);
        public ICommand btnCheck => new Command(async () => await CheckWordEntry());
        public ICommand btnCheckWordOne => new Command(async () => await CheckWordOne());
        public ICommand btnCheckWordTwo => new Command(async () => await CheckWordTwo());
        public ICommand btnCheckWordThree => new Command(async () => await CheckWordThree());

        #endregion Commands
    }
}
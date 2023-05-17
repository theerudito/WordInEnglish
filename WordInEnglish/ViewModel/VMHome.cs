using MarcTron.Plugin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using WordInEnglish.Application_Context;
using WordInEnglish.View;
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
            if (getLocalStorange() == "EN")
            {
                ChangeLanguage();
                Language = getLocalStorange();
            }
            else
            {
                ChangeLanguage();
                Language = getLocalStorange();
            }

            Navigation = navigation;

            LabelScore = 0;

            ScoreColor = ColorCorrect();

            InitialColor();

            GenerateWord();

            SelectWord();

            Score();
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

        // LANGUAGE
        private string _language;

        public string Language
        {
            get => _language;
            set => SetValue(ref _language, value);
        }

        private string _points;
        private string _answer;
        private string _checkYourWord;

        public string Points
        {
            get => _points;
            set => SetValue(ref _points, value);
        }

        public string Answer
        {
            get => _answer;
            set => SetValue(ref _answer, value);
        }

        public string CheckYourWord
        {
            get => _checkYourWord;
            set => SetValue(ref _checkYourWord, value);
        }

        // LANGUAGE

        public ImageSource ImageLanguage
        {
            get => _ImageSource;
            set => SetValue(ref _ImageSource, value);
        }

        #endregion Getters/Setters

        #region Methods

        public async Task GenerateWord()
        {
            if (Language == "EN") await GenerateWordEN();
            else await GenerateWordES();
        }

        public async Task GenerateWordEN()
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

        public async Task GenerateWordES()
        {
            try
            {
                var quantityOnTable = _Context.WordsES.ToListAsync().Result.Count;

                var random = new Random();

                var numsRandom = random.Next(1, quantityOnTable);

                var generateWord = await _Context.WordsES.Where(word => word.IdES == numsRandom).FirstOrDefaultAsync();

                IdWord = generateWord.IdES;

                LabelWord = generateWord.MyWord.ToUpper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task CheckWordEntry()
        {
            if (Language == "EN") await CheckWordEN();
            else await CheckWordES();
        }

        public async Task CheckWordEN()
        {
            await DisplayAlert("Language", getLocalStorange(), "OK");

            if (ValidationEntry() == true)
            {
                var Word = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

                if (Word != null)
                {
                    if (EntryWord.ToUpper().Trim() == Word.MyWord.ToUpper())
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
                        await AlertIncorrect();
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
                    await AlertWordNoExist();
                }
            }
        }

        public async Task CheckWordES()
        {
            await DisplayAlert("Language", getLocalStorange(), "OK");
            if (ValidationEntry() == true)
            {
                var Word = await _Context.WordsEN.Where(word => word.IdEN == IdWord).FirstOrDefaultAsync();

                if (Word != null)
                {
                    if (EntryWord.ToUpper().Trim() == Word.MyWord.ToUpper())
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
                        await AlertIncorrect();
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
                    await AlertWordNoExist();
                }
            }
        }

        public async Task AlertIncorrect()
        {
            if (Language == "EN") await DisplayAlert("Correct", "Try Again", "OK");
            else await DisplayAlert("Correct", "Intenta Otra Vez", "OK");
        }

        public async Task AlertWordNoExist()
        {
            if (Language == "EN") await DisplayAlert("Error", "Unregistered Word", "OK");
            else await DisplayAlert("Error", "Palabra No Registrada", "OK");
        }

        public async Task SelectWord()
        {
            if (Language == "EN") await WordRamdonEN();
            else await WordRamdonES();
        }

        public async Task WordRamdonEN()
        {
            List<int> numsRandom = numAletory(3);

            var wordCorrect = await _Context.WordsES.Where(word => word.IdES == IdWord).FirstOrDefaultAsync();

            var wordIncorrectOne = await _Context.WordsES.Where(word => word.IdES == numsRandom[0]).FirstOrDefaultAsync();
            var wordIncorrectTwo = await _Context.WordsES.Where(word => word.IdES == numsRandom[1]).FirstOrDefaultAsync();

            WordOne = wordCorrect.MyWord.ToUpper();

            WordTwo = wordIncorrectOne.MyWord.ToUpper();
            WordThree = wordIncorrectTwo.MyWord.ToUpper();

            var random = new Random();

            var numsRandomTwo = random.Next(1, 4);

            if (numsRandomTwo == 1)
            {
                WordOne = wordCorrect.MyWord.ToUpper();
                WordTwo = wordIncorrectOne.MyWord.ToUpper();
                WordThree = wordIncorrectTwo.MyWord.ToUpper();
                IdWordData[0] = IdWord;
            }
            else if (numsRandomTwo == 2)
            {
                WordOne = wordIncorrectOne.MyWord.ToUpper();
                WordTwo = wordCorrect.MyWord.ToUpper();
                WordThree = wordIncorrectTwo.MyWord.ToUpper();
                IdWordData[1] = IdWord;
            }
            else if (numsRandomTwo == 3)
            {
                WordOne = wordIncorrectOne.MyWord.ToUpper();
                WordTwo = wordIncorrectTwo.MyWord.ToUpper();
                WordThree = wordCorrect.MyWord.ToUpper();
                IdWordData[2] = IdWord;
            }
        }

        public async Task WordRamdonES()
        {
            List<int> numsRandom = numAletory(3);

            var wordCorrect = await _Context.WordsEN.Where(word => word.IdEN == IdWord).FirstOrDefaultAsync();

            var wordIncorrectOne = await _Context.WordsEN.Where(word => word.IdEN == numsRandom[0]).FirstOrDefaultAsync();
            var wordIncorrectTwo = await _Context.WordsEN.Where(word => word.IdEN == numsRandom[1]).FirstOrDefaultAsync();

            WordOne = wordCorrect.MyWord.ToUpper();

            WordTwo = wordIncorrectOne.MyWord.ToUpper();
            WordThree = wordIncorrectTwo.MyWord.ToUpper();

            var random = new Random();

            var numsRandomTwo = random.Next(1, 4);

            if (numsRandomTwo == 1)
            {
                WordOne = wordCorrect.MyWord.ToUpper();
                WordTwo = wordIncorrectOne.MyWord.ToUpper();
                WordThree = wordIncorrectTwo.MyWord.ToUpper();
                IdWordData[0] = IdWord;
            }
            else if (numsRandomTwo == 2)
            {
                WordOne = wordIncorrectOne.MyWord.ToUpper();
                WordTwo = wordCorrect.MyWord.ToUpper();
                WordThree = wordIncorrectTwo.MyWord.ToUpper();
                IdWordData[1] = IdWord;
            }
            else if (numsRandomTwo == 3)
            {
                WordOne = wordIncorrectOne.MyWord.ToUpper();
                WordTwo = wordIncorrectTwo.MyWord.ToUpper();
                WordThree = wordCorrect.MyWord.ToUpper();
                IdWordData[2] = IdWord;
            }
        }

        public async Task CheckWordOne()
        {
            if (Language == "EN") await CheckWordOneEN();
            else await CheckWordOneES();
        }

        public async Task CheckWordOneEN()
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

        public async Task CheckWordOneES()
        {
            var Word = await _Context.WordsEN.Where(word => word.IdEN == IdWord).FirstOrDefaultAsync();

            if (IdWordData[0] == Word.IdEN)
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
            if (Language == "EN") await CheckWordTwoEN();
            else await CheckWordTwoES();
        }

        public async Task CheckWordTwoEN()
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

        public async Task CheckWordTwoES()
        {
            var Word = await _Context.WordsEN.Where(word => word.IdEN == IdWord).FirstOrDefaultAsync();
            if (IdWordData[1] == Word.IdEN)
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
            if (Language == "EN") await CheckWordThreeEN();
            else await CheckWordThreeES();
        }

        public async Task CheckWordThreeEN()
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

        public async Task CheckWordThreeES()
        {
            var Word = await _Context.WordsEN.Where(word => word.IdEN == IdWord).FirstOrDefaultAsync();

            if (IdWordData[2] == Word.IdEN)
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
            var audio = "WordInEnglish.Sound.correct.mp3";
            SoundPlay(audio);
        }

        public void SoundInCorrect()
        {
            var audio = "WordInEnglish.Sound.error.mp3";
            SoundPlay(audio);
        }

        public void SoundPlay(string audio)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream(audio);
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

            if (Language == "EN")
            {
                ImageLanguage = imageEN;
                Xamarin.Essentials.Preferences.Set("language", "ES");
                SelectLanguage();
            }
            else
            {
                ImageLanguage = imageES;
                Xamarin.Essentials.Preferences.Set("language", "EN");
                SelectLanguage();
            }
        }

        public void SelectLanguage()
        {
            getLocalStorange();
            if (Language == "EN")
            {
                Points = MyLanguages._Points;
                Answer = MyLanguages._Answer;
                CheckYourWord = MyLanguages._Check_Your_Word;
            }
            else
            {
                Points = MyLanguages._Puntos;
                Answer = MyLanguages._Respuesta;
                CheckYourWord = MyLanguages._Revisa_Tu_Palabra;
            }
        }

        public string getLocalStorange()
        {
            var language = Xamarin.Essentials.Preferences.Get("language", "EN");
            return Language = language;
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

        public async Task GoConfig()
        {
            ShowIntertiscal();
            if (CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                // Mostrar el anuncio intersticial
                CrossMTAdmob.Current.ShowInterstitial();
                await Navigation.PushAsync(new Config());
            }
        }

        public void ShowIntertiscal()
        {
            var idIntersticial = "ca-app-pub-7633493507240683/8231562165";

            CrossMTAdmob.Current.LoadInterstitial(idIntersticial);

        }

        public void ShowVideoAds()
        {
            var idVideoIntersticial = "ca-app-pub-7633493507240683/9925478281";

            // Cargar el anuncio de video recompensado
            CrossMTAdmob.Current.LoadRewardedVideo(idVideoIntersticial);


            // Verificar si el anuncio está listo para mostrarse
            if (CrossMTAdmob.Current.IsRewardedVideoLoaded())
            {
                // Mostrar el anuncio de video recompensado
                CrossMTAdmob.Current.ShowRewardedVideo();
            }
        }

        #endregion Methods

        #region Commands

        public ICommand btnGoConfig => new Command(async () => await GoConfig());
        public ICommand btnChangeLanguage => new Command(ChangeLanguage);
        public ICommand btnCheck => new Command(async () => await CheckWordEntry());
        public ICommand btnCheckWordOne => new Command(async () => await CheckWordOne());
        public ICommand btnCheckWordTwo => new Command(async () => await CheckWordTwo());
        public ICommand btnCheckWordThree => new Command(async () => await CheckWordThree());

        #endregion Commands
    }
}
using System.IO;
using System.Reflection;

namespace WordInEnglish.Helpers
{
    public static class SoundsApp
    {
        public static void SoundCorrect()
        {
            var audio = "WordInEnglish.Sound.correct.mp3";
            SoundPlay(audio);
        }

        public static void SoundInCorrect()
        {
            var audio = "WordInEnglish.Sound.error.mp3";
            SoundPlay(audio);
        }

        private static void SoundPlay(string audio)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream(audio);
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
        }
    }
}
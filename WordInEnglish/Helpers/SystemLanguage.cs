using Plugin.Multilingual;
using Plugin.Multilingual.Abstractions;
using System;
using System.Globalization;

namespace WordInEnglish.Helpers
{
    public class SystemLanguage : IMultilingual
    {
        public CultureInfo CurrentCultureInfo { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CultureInfo DeviceCultureInfo => throw new NotImplementedException();

        public CultureInfo[] CultureInfoList => throw new NotImplementedException();

        public CultureInfo[] NeutralCultureInfoList => throw new NotImplementedException();

        public event EventHandler<CultureInfoChangedEventArgs> OnCultureInfoChanged;

        public CultureInfo GetCultureInfo(string name)
        {
            var netLanguage = "en"; // Idioma predeterminado

            if (CrossMultilingual.Current.DeviceCultureInfo != null)
            {
                netLanguage = CrossMultilingual.Current.DeviceCultureInfo.Name;
            }

            return new CultureInfo(netLanguage);
        }
    }
}
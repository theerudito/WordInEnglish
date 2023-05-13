using WordInEnglish.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordInEnglish.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Config : ContentPage
    {
        public Config()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new VMConfig(Navigation);
        }
    }
}
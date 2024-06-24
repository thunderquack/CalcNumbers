namespace CountingWhiz
{
    using CountingWhiz.Resources.Strings;
    using Microsoft.Maui.Controls;
    using System.Globalization;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var culture = Preferences.Get("AppLanguage", CultureInfo.CurrentCulture.Name);
            SetCulture(culture);

            MainPage = new NavigationPage(new MainPage());
        }

        public static void SetCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            AppResources.Culture = culture;
        }
    }
}
namespace CountingWhiz
{
    using CountingWhiz.Resources.Strings;

    /// <summary>
    /// Settings screen.
    /// </summary>
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LanguagePicker.SelectedItem = Preferences.Get("AppLanguage", "en");
            MaxScoreEntry.Text = Preferences.Get("MaxScore", 10).ToString();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (int.TryParse(MaxScoreEntry.Text, out int maxScore) && maxScore > 0)
            {
                Preferences.Set("MaxScore", maxScore);
                Preferences.Set("AppLanguage", LanguagePicker.SelectedItem.ToString());
                App.SetCulture(Preferences.Get("AppLanguage", "en"));
                App.UpdateMainPage();
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please enter a valid number greater than 0.", "OK");
            }
        }
    }
}
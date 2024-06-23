namespace CalcNumbers
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (int.TryParse(MaxScoreEntry.Text, out int maxScore) && maxScore > 0)
            {
                Preferences.Set("MaxScore", maxScore);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Input", "Please enter a valid number greater than 0.", "OK");
            }
        }
    }
}
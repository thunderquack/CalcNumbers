namespace CountingWhiz
{
    using CountingWhiz.Resources.Strings;
    using Microsoft.Maui.Controls;
    using Microsoft.Maui.Graphics;

    /// <summary>
    /// Main screen.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        private int correctAnswer;
        private int score;
        private string pageTitle;
        private string resultPrefix;
        private string resultExample;
        private Color resultColor;
        private Color progressColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            GenerateRandomExample();
            UpdateProgress();
        }

        /// <summary>
        /// Gets or sets the Page Header.
        /// </summary>
        public string PageTitle
        {
            get => pageTitle;
            set
            {
                pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public string ResultPrefix
        {
            get => resultPrefix;
            set
            {
                resultPrefix = value;
                OnPropertyChanged(nameof(ResultPrefix));
            }
        }

        public string ResultExample
        {
            get => resultExample;
            set
            {
                resultExample = value;
                OnPropertyChanged(nameof(ResultExample));
            }
        }

        public Color ResultColor
        {
            get => resultColor;
            set
            {
                resultColor = value;
                OnPropertyChanged(nameof(ResultColor));
            }
        }

        public Color ProgressColor
        {
            get => progressColor;
            set
            {
                progressColor = value;
                OnPropertyChanged(nameof(ProgressColor));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateProgress();
        }

        private int MaxScore => Preferences.Get("MaxScore", 10);

        private void GenerateRandomExample()
        {
            var random = new Random();
            bool isAddition = random.Next(2) == 0;

            if (isAddition)
            {
                int num1;
                int num2;
                do
                {
                    num1 = random.Next(0, 21);
                    num2 = random.Next(0, 21);
                    correctAnswer = num1 + num2;
                }
                while (correctAnswer > 20);
                ExampleLabel.Text = $"{num1} + {num2} = ?";
            }
            else
            {
                int num1 = random.Next(0, 21);
                int num2 = random.Next(0, 21);

                // Ensure non-negative result for subtraction
                if (num1 < num2)
                {
                    (num2, num1) = (num1, num2);
                }

                correctAnswer = num1 - num2;
                ExampleLabel.Text = $"{num1} - {num2} = ?";
            }
        }

        private void OnCheckAnswerClicked(object sender, EventArgs e)
        {
            if (int.TryParse(AnswerEntry.Text, out int userAnswer))
            {
                if (userAnswer == correctAnswer)
                {
                    ResultPrefix = $"{AppResources.Correct}";
                    ResultExample = string.Empty;
                    ResultColor = Colors.Green;
                    score++;
                }
                else
                {
                    string example = ExampleLabel.Text.Replace("?", correctAnswer.ToString());
                    ResultPrefix = $"{AppResources.Incorrect}";
                    ResultExample = $"{example}";
                    ResultColor = Colors.Red;
                    score--;
                }

                // Ensure score stays within bounds
                score = Math.Max(0, Math.Min(score, MaxScore));

                UpdateProgress();
            }
            else
            {
                ResultPrefix = $"{AppResources.ValidNumber}";
                ResultExample = string.Empty;
                ResultColor = Colors.Red;
            }

            // Generate a new example for the next attempt
            GenerateRandomExample();
            AnswerEntry.Text = string.Empty;
            AnswerEntry.Focus();
        }

        private void UpdateProgress()
        {
            double progress = (double)score / MaxScore;

            // Update ProgressBar
            ProgressBar.Progress = progress;

            // Update the color of the ProgressBar based on progress
            if (progress <= 0.5)
            {
                ProgressColor = Colors.Red;
            }
            else if (progress < 1.0)
            {
                ProgressColor = Colors.Yellow;
            }
            else
            {
                ProgressColor = Colors.Green;
            }

            // Update StarsLabel
            StarsLabel.Text = $"{AppResources.CorrectAnswers}: {score}";

            // Update Page Title with MaxScore
            PageTitle = $"{AppResources.MaxScore}: {MaxScore}";
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
namespace CalcNumbers
{
    public partial class MainPage : ContentPage
    {
        private int correctAnswer;
        private int score;
        private const int maxScore = 10;

        public MainPage()
        {
            InitializeComponent();
            GenerateRandomExample();
            UpdateProgress();
        }

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
                } while (correctAnswer > 20);
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
                    ResultLabel.Text = "Correct!";
                    score++;
                }
                else
                {
                    ResultLabel.Text = $"Incorrect. The correct answer is {correctAnswer}.";
                    score--;
                }

                // Ensure score stays within bounds
                score = Math.Max(0, Math.Min(score, maxScore));

                UpdateProgress();
            }
            else
            {
                ResultLabel.Text = "Please enter a valid number.";
            }

            // Generate a new example for the next attempt
            GenerateRandomExample();
            AnswerEntry.Text = string.Empty;
        }

        private void UpdateProgress()
        {
            // Update ProgressBar
            ProgressBar.Progress = (double)score / maxScore;

            // Update StarsLabel
            StarsLabel.Text = $"Stars: {score}";
        }
    }
}
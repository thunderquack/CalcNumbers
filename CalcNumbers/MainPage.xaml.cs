namespace CalcNumbers
{
    public partial class MainPage : ContentPage
    {
        private int correctAnswer;

        public MainPage()
        {
            InitializeComponent();
            GenerateRandomExample();
        }

        private void GenerateRandomExample()
        {
            var random = new Random();
            bool isAddition = random.Next(2) == 0;
            int num1 = random.Next(0, 21);
            int num2 = random.Next(0, 21);

            if (isAddition)
            {
                correctAnswer = num1 + num2;
                ExampleLabel.Text = $"{num1} + {num2} = ?";
            }
            else
            {
                // Ensure non-negative result for subtraction
                if (num1 < num2)
                {
                    var temp = num1;
                    num1 = num2;
                    num2 = temp;
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
                }
                else
                {
                    ResultLabel.Text = $"Incorrect. The correct answer is {correctAnswer}.";
                }
            }
            else
            {
                ResultLabel.Text = "Please enter a valid number.";
            }

            // Generate a new example for the next attempt
            GenerateRandomExample();
            AnswerEntry.Text = string.Empty;
        }
    }
}

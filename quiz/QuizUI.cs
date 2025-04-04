using System;

namespace Quiz
{
    public class QuizUI
    {
        private const string YesResponse = "yes";
        private const string DoneResponse = "done";
        private const int ExitOption = 3;
        private const int IndexOffset = 1;
        private const int CreateQuizOption = 1;
        private const int TakeQuizOption = 2;

        public int ShowMenu()
        {
            Console.WriteLine("\n--- Quiz System ---");
            Console.WriteLine("1. Create a Quiz");
            Console.WriteLine("2. Take a Quiz");
            Console.WriteLine("3. Exit");

            return GetMenuChoice();
        }

        private int GetMenuChoice()
        {
            return ReadIntegerInput("Choose an option: ");
        }

        public List<Question> CreateQuiz(List<Question> questions)
        {
            ShowMessage("\n--- Create a New Quiz ---");

            while (true)
            {
                questions.Add(GetQuestionFromUser());
                if (!ConfirmAction("Do you want to add another question? (yes/no): ")) break;
            }

            ShowMessage("Quiz saved successfully!");
            return questions;
        }

        private Question GetQuestionFromUser()
        {
            Console.Write("Enter the question text: ");
            string questionText = Console.ReadLine();

            List<string> answers = ReadAnswers();
            List<int> correctAnswerIndices = ReadIntegerList("Enter the correct answer numbers (comma-separated, starting from 1): ");

            return new Question(questionText, answers, correctAnswerIndices);
        }

        private List<string> ReadAnswers()
        {
            List<string> answers = new();
            ShowMessage("Enter possible answers (type 'done' when finished):");

            while (true)
            {
                string answer = Console.ReadLine();
                if (answer.Trim().Equals(DoneResponse, StringComparison.OrdinalIgnoreCase)) break;
                answers.Add(answer);
            }
            return answers;
        }

        public void TakeQuiz(List<Question> questions)
        {
            if (!questions.Any())
            {
                ShowMessage("No questions available. Please create a quiz first.");
                return;
            }

            ShowMessage("\n--- Starting the Quiz ---");
            int score = RunQuiz(questions);
            ShowQuizResult(score, questions.Count);
        }

        private int RunQuiz(List<Question> questions)
        {
            int score = 0;
            Random random = new();
            var shuffledQuestions = questions.OrderBy(q => random.Next()).ToList();

            foreach (var question in shuffledQuestions)
            {
                ShowQuestion(question);
                var userAnswers = ReadIntegerList("Enter your answers (comma-separated, starting from 1): ");

                if (QuizLogic.CheckAnswer(question, userAnswers))
                {
                    ShowMessage("Correct!");
                    score++;
                }
                else
                {
                    ShowMessage("Incorrect!");
                }
            }
            return score;
        }

        private void ShowQuestion(Question question)
        {
            ShowMessage($"\n{question.QuestionText}");
            for (int i = 0; i < question.Answers.Count; i++)
            {
                ShowMessage($"{i + IndexOffset}. {question.Answers[i]}");
            }
        }

        private void ShowQuizResult(int score, int totalQuestions)
        {
            ShowMessage($"\nQuiz finished! Your score: {score}/{totalQuestions}");
        }

        public void DisplayQuestion(Question question)
        {
            ShowMessage(question.ToString());
        }

        private List<int> ReadIntegerList(string prompt)
        {
            ShowMessage(prompt);
            while (true)
            {
                try
                {
                    return Console.ReadLine()
                        .Split(',')
                        .Select(x => int.Parse(x.Trim()) - IndexOffset)
                        .ToList();
                }
                catch
                {
                    ShowMessage("Invalid input. Please enter numbers separated by commas.");
                }
            }
        }

        private int ReadIntegerInput(string prompt)
        {
            ShowMessage(prompt);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                    return choice;

                ShowMessage("Invalid input. Please enter a valid number.");
            }
        }

        private bool ConfirmAction(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().Trim().Equals(YesResponse, StringComparison.OrdinalIgnoreCase);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void ShowGoodbyeMessage()
        {
            Console.WriteLine("Goodbye!");
        }

        public void ShowInvalidOption()
        {
            Console.WriteLine("Invalid option. Try again.");
        }
        public void ShowLoadErrorMessage()
        {
            Console.WriteLine("⚠️ Failed to load questions. Please check your quiz file or try again later.");
        }
    }
}

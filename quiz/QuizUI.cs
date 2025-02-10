using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class QuizUI
    {
        private readonly QuizLogic _quizLogic;

        public QuizUI(QuizLogic quizLogic)
        {
            _quizLogic = quizLogic;
        }

        // Show the main menu and handle user input for different actions
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Quiz System ---");
                Console.WriteLine("1. Create a Quiz");
                Console.WriteLine("2. Take a Quiz");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateQuiz();
                        break;
                    case "2":
                        TakeQuiz();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        // Method to create a quiz by adding questions
        public void CreateQuiz()
        {
            var questions = _quizLogic.LoadQuestions();
            Console.WriteLine("\n--- Create a New Quiz ---");

            while (true)
            {
                Console.Write("Enter the question text: ");
                string questionText = Console.ReadLine();

                List<string> answers = new List<string>();
                Console.WriteLine("Enter possible answers (type 'done' when finished):");

                while (true)
                {
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "done") break;
                    answers.Add(answer);
                }

                Console.WriteLine("Enter the correct answer numbers (comma-separated, starting from 1):");
                List<int> correctAnswerIndices = Console.ReadLine()
                                                        .Split(',')
                                                        .Select(x => int.Parse(x.Trim()) - 1)
                                                        .ToList();

                _quizLogic.AddQuestion(questions, questionText, answers, correctAnswerIndices);

                Console.Write("Do you want to add another question? (yes/no): ");
                if (Console.ReadLine().ToLower() != "yes") break;
            }

            _quizLogic.SaveQuestions(questions);
            Console.WriteLine("Quiz saved successfully!");
        }

        // Method to allow the user to take the quiz
        public void TakeQuiz()
        {
            List<Question> questions = _quizLogic.LoadQuestions();

            if (questions.Count == 0)
            {
                Console.WriteLine("No questions available. Please create a quiz first.");
                return;
            }

            Console.WriteLine("\n--- Starting the Quiz ---");
            int score = 0;

            Random random = new Random();
            List<Question> shuffledQuestions = questions.OrderBy(q => random.Next()).ToList();

            foreach (var question in shuffledQuestions)
            {
                Console.WriteLine($"\n{question.Text}");
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i]}");
                }

                Console.Write("Enter your answers (comma-separated, starting from 1): ");
                List<int> userAnswers = Console.ReadLine()
                                               .Split(',')
                                               .Select(x => int.Parse(x.Trim()) - 1)
                                               .ToList();

                if (_quizLogic.CheckAnswer(question, userAnswers))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                }
            }

            Console.WriteLine($"\nQuiz finished! Your score: {score}/{questions.Count}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz
{
    public class QuizUI
    {

        public int ShowMenu()
        {
            Console.WriteLine("\n--- Quiz System ---");
            Console.WriteLine("1. Create a Quiz");
            Console.WriteLine("2. Take a Quiz");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
                return choice;

            return -1; 
        }

        public List<Question> CreateQuiz(List<Question> questions)
        {
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

                Console.Write("Enter the correct answer numbers (comma-separated, starting from 1): ");
                List<int> correctAnswerIndices = ReadIntList();

                QuizLogic.AddQuestion(questions, questionText, answers, correctAnswerIndices);

                Console.Write("Do you want to add another question? (yes/no): ");
                if (Console.ReadLine().ToLower() != "yes") break;
            }

            Console.WriteLine("Quiz saved successfully!");
            return questions; 
        }

        public void TakeQuiz(List<Question> questions)
        {
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
                Console.WriteLine($"\n{question.QuestionText}");
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i]}");
                }

                Console.Write("Enter your answers (comma-separated, starting from 1): ");
                List<int> userAnswers = ReadIntList();

                if (QuizLogic.CheckAnswer(question, userAnswers))
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

        private List<int> ReadIntList()
        {
            while (true)
            {
                try
                {
                    return Console.ReadLine()
                        .Split(',')
                        .Select(x => int.Parse(x.Trim()) - 1)
                        .ToList();
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter numbers separated by commas.");
                }
            }
        }
    }
}
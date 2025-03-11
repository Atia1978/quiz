using static System.Net.Mime.MediaTypeNames;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Question> questions = QuizLogic.LoadQuestions();
            QuizUI quizUI = new QuizUI();

            while (true)
            {
                int choice = quizUI.ShowMenu();
                switch (choice)
                {
                    case 1:
                        questions = quizUI.CreateQuiz(questions);
                        QuizLogic.SaveQuestions(questions);
                        break;
                    case 2:
                        quizUI.TakeQuiz(questions);
                        break;
                    case 3:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}

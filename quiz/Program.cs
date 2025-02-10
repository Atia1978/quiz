using static System.Net.Mime.MediaTypeNames;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuizLogic quizLogic = new QuizLogic();
             QuizUI quizUI = new QuizUI(quizLogic);

            quizUI.ShowMenu();

            
        }
    }
}

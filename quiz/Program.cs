using Quiz;
using static System.Net.Mime.MediaTypeNames;

List<Question> questions = QuizLogic.LoadQuestions();
QuizUI quizUI = new QuizUI();

if (questions.Count == 0)
{
    quizUI.ShowLoadErrorMessage();
}

while (true)
{
    MenuOption choice = (MenuOption)quizUI.ShowMenu();

    switch (choice)
    {
        case MenuOption.CreateQuiz:
            questions = quizUI.CreateQuiz(questions);
            QuizLogic.SaveQuestions(questions);
            break;
        case MenuOption.TakeQuiz:
            quizUI.TakeQuiz(questions);
            break;
        case MenuOption.Exit:
            quizUI.ShowGoodbyeMessage();
            return;
        default:
            quizUI.ShowInvalidOption();
            break;
    }
}


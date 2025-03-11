

namespace Quiz
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public List<int> CorrectAnswers { get; set; }
        public Question()
        {
            Answers = new List<string>();
            CorrectAnswers = new List<int>();
        }

        public Question(string text, List<string> answers, List<int> correctAnswerIndices)
        {
            QuestionText = text;
            Answers = answers;
            CorrectAnswers = correctAnswerIndices;
        }

        public bool IsCorrect(List<int> userAnswers)
        {
            return new HashSet<int>(CorrectAnswers).SetEquals(userAnswers);
        }
        public void DisplayQuestion()
        {
            Console.WriteLine(QuestionText);
            for (int i = 0; i < Answers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Answers[i]}");
            }
        }

    }
}

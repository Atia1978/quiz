

using System.Text;

namespace Quiz
{
    public class Question
    {
        private const int DisplayIndexOffset = 1;
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
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(QuestionText);
            for (int i = 0; i < Answers.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {Answers[i]}");
            }
            return sb.ToString();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public List<int> CorrectAnswerIndices { get; set; }
        public Question() 
        {
            Answers = new List<string>();
            CorrectAnswerIndices = new List<int>();
        }

        public Question(string text, List<string> answers, List<int> correctAnswerIndices)
        {
            Text = text;
            Answers = answers;
            CorrectAnswerIndices = correctAnswerIndices;
        }

        public bool IsCorrect(List<int> userAnswers)
        {
            userAnswers.Sort();
            CorrectAnswerIndices.Sort();
            return CorrectAnswerIndices.SequenceEqual(userAnswers);
        }
        //public bool IsCorrect(List<int> userAnswers)
        //{
        //    return CorrectAnswerIndices.OrderBy(x => x).SequenceEqual(userAnswers.OrderBy(x => x));
        //}
    }
}

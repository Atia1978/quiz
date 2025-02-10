using System;
using System.Xml.Serialization;

namespace Quiz
{
    public class QuizLogic
    {
        private const string FileName = @"C:\Users\abdal\source\repos\quiz\quiz_questions.xml";

        public List<Question> LoadQuestions()
        {
            
            if (!File.Exists(FileName))
            {
                return new List<Question>();
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Question>));

            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                return (List<Question>)formatter.Deserialize(fs);
            }
           
        }

        public void SaveQuestions(List<Question> questions)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Question>));

            using (FileStream fs = new FileStream(FileName, FileMode.Create))
            {
                formatter.Serialize(fs, questions);
            }
        }

        public void AddQuestion(List<Question> questions, string text, List<string> answers, List<int> correctAnswerIndices)
        {
            var question = new Question(text, answers, correctAnswerIndices);
            questions.Add(question);
        }

        public bool CheckAnswer(Question question, List<int> userAnswers)
        {
            return question.IsCorrect(userAnswers);
        }

        public Question GetRandomQuestion(List<Question> questions)
        {
            Random random = new Random();
            int index = random.Next(questions.Count);
            return questions[index];
        }
    }
}

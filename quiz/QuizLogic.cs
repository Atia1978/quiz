using System;
using System.Xml.Serialization;

namespace Quiz
{
    public static class QuizLogic
    {
        private static readonly string FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quiz_questions.xml");

        public static List<Question> LoadQuestions()
        {
            if (!File.Exists(FileName))
            {
                return new List<Question>();
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Question>));

            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Open))
                {
                    return (List<Question>)formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading questions: " + ex.Message);
                return new List<Question>();
            }
        }

        public static void SaveQuestions(List<Question> questions)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Question>));

            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    formatter.Serialize(fs, questions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving questions: " + ex.Message);
            }
        }

        public static void AddQuestion(List<Question> questions, string text, List<string> answers, List<int> correctAnswerIndices)
        {
            var question = new Question(text, answers, correctAnswerIndices);
            questions.Add(question);
        }

        public static bool CheckAnswer(Question question, List<int> userAnswers)
        {
            return question.IsCorrect(userAnswers);
        }

        public static Question GetRandomQuestion(List<Question> questions)
        {
            if (questions.Count == 0)
            {
                throw new InvalidOperationException("No questions available.");
            }

            Random random = new Random();
            int index = random.Next(questions.Count);
            return questions[index];
        }
    }
}

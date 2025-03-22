using System;
using System.Xml.Serialization;

namespace Quiz
{
    public static class QuizLogic
    {
        private static readonly string FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "quiz_questions.xml");
        private static readonly Random RandomGenerator = new Random(); // Static random instance

        public static List<Question> LoadQuestions()
        {
            if (!File.Exists(FileName))
            {
                return CreateEmptyQuestionList();
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
                HandleFileError("loading questions", ex);
                return CreateEmptyQuestionList();
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
                HandleFileError("saving questions", ex);
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
            if (IsEmpty(questions))
            {
                throw new InvalidOperationException("No questions available.");
            }

            int index = RandomGenerator.Next(questions.Count);
            return questions[index];
        }

        private static List<Question> CreateEmptyQuestionList()
        {
            return new List<Question>(); // Improved readability
        }

        private static bool IsEmpty(List<Question> questions)
        {
            return !questions.Any(); // More intuitive than Count == 0
        }

        private static void HandleFileError(string action, Exception ex)
        {
            Console.WriteLine($"Error {action}: {ex.Message}");
        }
    }
}

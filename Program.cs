class Program
{
    private static readonly IQuestionRepository questionRepository = new QuestionRepository();
    private static readonly IQuizService quizService = new QuizService(questionRepository, new Random());

    public static void Main(string[] args)
    {
        var questions = quizService.GetQuestions(5);

        double score = 0;
        foreach (var question in questions)
        {
            var isCorrect = quizService.AskQuestion(question, out bool secondChanceUsed);
            score += quizService.CalculateScore(isCorrect, secondChanceUsed);
        }

        Console.WriteLine($"Your score: {score}/5");
    }
}

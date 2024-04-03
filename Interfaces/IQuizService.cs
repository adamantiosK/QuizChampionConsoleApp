public interface IQuizService
{
    IEnumerable<QuizQuestion> GetQuestions(int amount);
    bool AskQuestion(QuizQuestion question, out bool secondChanceUsed);
    double CalculateScore(bool isCorrect, bool secondChanceUsed);
}
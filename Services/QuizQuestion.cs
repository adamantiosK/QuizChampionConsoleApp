public class QuizService : IQuizService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly Random _random;

    public QuizService(IQuestionRepository questionRepository, Random random)
    {
        _questionRepository = questionRepository;
        _random = random;
    }

    public IEnumerable<QuizQuestion> GetQuestions(int amount)
    {
        var allQuestions = _questionRepository.GetAllQuestions();
        return allQuestions.OrderBy(q => _random.Next()).Take(amount);
    }

    public bool AskQuestion(QuizQuestion question, out bool secondChanceUsed)
    {
        secondChanceUsed = false;
        Random rng = new Random();
        var shuffledAnswers = question.Answers.OrderBy(a => rng.Next()).ToList();
        int correctAnswerIndex = shuffledAnswers.IndexOf(question.Correct_answer) + 1;

        Console.WriteLine(question.Question);
        for (int i = 0; i < shuffledAnswers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {shuffledAnswers[i]}");
        }

        Console.Write("Your answer (number): ");
        int userAnswer = GetUserAnswer(shuffledAnswers.Count);

        if (userAnswer == correctAnswerIndex)
        {
            Console.WriteLine("Correct!");
            return true;
        }
        else
        {
            Console.WriteLine("Wrong! Try once more:");
            secondChanceUsed = true;
            userAnswer = GetUserAnswer(shuffledAnswers.Count);
            if (userAnswer == correctAnswerIndex)
            {
                Console.WriteLine("Correct on the second try!");
                return true;
            }
            else
            {
                Console.WriteLine("Wrong again.");
                return false;
            }
        }
    }

    public double CalculateScore(bool isCorrect, bool secondChanceUsed)
    {
        if (isCorrect && !secondChanceUsed) return 1;
        if (isCorrect) return 0.5;
        return 0;
    }

    private int GetUserAnswer(int answerCount)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int input) && input >= 1 && input <= answerCount)
            {
                return input;
            }
            else
            {
                Console.Write($"Please enter a number between 1 and {answerCount}: ");
            }
        }
    }
}
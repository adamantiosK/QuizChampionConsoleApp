using Newtonsoft.Json;
using System.Reflection;
using System.IO;

public class QuestionRepository : IQuestionRepository
{
    private readonly static string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private readonly string _filePath = Path.Combine(exePath, "..","..","..", "Data" , "quizQuestions.json");

    public IEnumerable<QuizQuestion> GetAllQuestions()
    {
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException("Questions file not found.", _filePath);
        }

        string json = File.ReadAllText(_filePath);
        var quizResponse = JsonConvert.DeserializeObject<QuizResponse>(json);
        return quizResponse?.Results ?? new List<QuizQuestion>();
    }
}
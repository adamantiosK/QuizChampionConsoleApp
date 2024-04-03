using System.Text.Json.Serialization;

public class QuizQuestion
{
    public required string Type { get; set; }
    public required string Difficulty { get; set; }
    public required string Category { get; set; }
    public required string Question { get; set; }
    public required string Correct_answer { get; set; }
    public required List<string> Incorrect_answers { get; set; }

    [JsonIgnore]
    public List<string> Answers => new List<string> { Correct_answer }.Concat(Incorrect_answers).ToList();

}

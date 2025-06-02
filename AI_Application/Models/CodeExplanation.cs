namespace AI_Application.Models
{
    public class CodeExplanation
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string CodeSnippet { get; set; }
        public string? Explanation { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

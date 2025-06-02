namespace AI_Application.Models
{
    public class Progress
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string Topic { get; set; }
        public int CompletionPercentage { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

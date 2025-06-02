namespace AI_Application.Models
{
    public class ExerciseQuestion
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string Question { get; set; }
        public string? Answer { get; set; }
        public DateTime AskedAt { get; set; }
    }
}

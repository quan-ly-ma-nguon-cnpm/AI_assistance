namespace AI_Application.Models
{
    public class InformationLookup
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string Query { get; set; }
        public string? Result { get; set; }
        public DateTime SearchedAt { get; set; }
    }
}

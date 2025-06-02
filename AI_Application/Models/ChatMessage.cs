namespace AI_Application.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string Message { get; set; }
        public required string Response { get; set; }
        public DateTime SentAt { get; set; }
    }
}

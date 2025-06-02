namespace AI_Application.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateJoined { get; set; }
    }
}

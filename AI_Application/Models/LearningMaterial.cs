namespace AI_Application.Models
{
    public class LearningMaterial
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime UploadDate { get; set; }
        public string? UploadedBy { get; set; }
    }
}

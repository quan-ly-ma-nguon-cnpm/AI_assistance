namespace AI_Application.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public required string FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

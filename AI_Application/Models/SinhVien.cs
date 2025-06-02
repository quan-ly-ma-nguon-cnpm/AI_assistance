namespace AI_Application.Models.SinhVien
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }

    public class CodeExplanation
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
    }

    public class ExerciseQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class LearningMaterial
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class InformationLookup
    {
        public int Id { get; set; }
        public string Query { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }

    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
    }
}

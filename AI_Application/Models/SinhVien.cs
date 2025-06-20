// Gộp tất cả các model SinhVien vào một file duy nhất để dễ quản lý
using System;
using Microsoft.EntityFrameworkCore;

namespace AI_Application.Models.SinhVien
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.Now;
    }

    public class CodeExplanation
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }

    public class ExerciseQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public DateTime AskedAt { get; set; } = DateTime.Now;
    }

    public class LearningMaterial
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
    }

    public class LearningProgress
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public double CompletionPercentage { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    public class InformationLookup
    {
        public int Id { get; set; }
        public string Query { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public DateTime SearchedAt { get; set; } = DateTime.Now;
    }

    public class UploadedDocument
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }

    public class SavedDocument
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime SavedAt { get; set; } = DateTime.Now;
    }

    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
    }
}


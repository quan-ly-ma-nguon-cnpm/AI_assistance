using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AI_Application.Models.Users;
using AI_Application.Models;
using AI_Application.Models.SinhVien; // Thêm để gọi các model SinhVien

namespace AI_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Các bảng đã có
        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<CauHoi> CauHois { get; set; } = default!;
        public DbSet<PhanHoi> PhanHois { get; set; } = default!;
        public DbSet<PhanHoiCauHoi> PhanHoiCauHois { get; set; } = default!;


        // ✅ Thêm các bảng từ phần Sinh Viên
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;
        public DbSet<CodeExplanation> CodeExplanations { get; set; } = default!;
        public DbSet<ExerciseQuestion> ExerciseQuestions { get; set; } = default!;
        public DbSet<LearningMaterial> LearningMaterials { get; set; } = default!;
        public DbSet<LearningProgress> LearningProgresses { get; set; } = default!;
        public DbSet<InformationLookup> InformationLookups { get; set; } = default!;
        public DbSet<Document> Documents { get; set; } = default!;
        public DbSet<UploadedDocument> UploadedDocuments { get; set; } = default!;
        public DbSet<SavedDocument> SavedDocuments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

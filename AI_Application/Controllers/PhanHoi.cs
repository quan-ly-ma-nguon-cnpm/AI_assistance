using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Application.Models
{
    public class PhanHoi // ← THÊM từ khóa public ở đây
    {
        public int Id { get; set; }
        public string NoiDung { get; set; } = string.Empty;
        public string NguoiGui { get; set; } = string.Empty;
        public string TieuDe { get; set; } = string.Empty;
        public string LinhVuc { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
        public DateTime ThoiGianGui { get; set; }
        public bool DaDuyet { get; set; }
        public string? NguoiNhan { get; internal set; }
        public int CauHoiId { get; set; } 
        public CauHoi? CauHoi { get; set; } 

        [NotMapped]
        public object? ThoiGianPhanHoi { get; internal set; }
    }
}

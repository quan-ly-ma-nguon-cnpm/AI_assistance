using System;

namespace AI_Application.Models
{
    public class PhanHoiCauHoi
    {
        public int Id { get; set; }
        public string NoiDung { get; set; } = string.Empty;
        public string NguoiGui { get; set; } = string.Empty; // tên giảng viên
        public string NguoiNhan { get; set; } = string.Empty; // tên sinh viên
        public int CauHoiId { get; set; } // Liên kết với câu hỏi
    
        public CauHoi? CauHoi { get; set; } 
        public bool DaDuyet { get; set; } = false;
        public string TieuDe { get; set; } = string.Empty;


        public DateTime ThoiGianPhanHoi { get; set; } = DateTime.Now;
    }
}

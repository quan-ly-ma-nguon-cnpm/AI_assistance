namespace AI_Application.Models
{
    public class PhanHoiViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; } = string.Empty;
        public string LinhVuc { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
        public string NoiDung { get; set; } = string.Empty; // Initialize with a default value
        public string NguoiGui { get; set; } = string.Empty; // Initialize with a default value
        public DateTime ThoiGianGui { get; set; }
        public bool DaDuyet { get; set; }
        public DateTime? ThoiGianDat { get; set; }
    }
}
namespace AI_Application.Models
{
    public class CauHoiViewModel
    {
        public int Id { get; set; } 
        public string TieuDe { get; set; }
        public string LinhVuc { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? ThoiGianDat { get; set; }
        public DateTime NgayTao { get; set; } 
        public string NguoiGui { get; set; }
        public bool DaDuyet { get; set; }

        public CauHoiViewModel()
        {
            TieuDe = string.Empty;
            LinhVuc = string.Empty;
            NguoiGui = string.Empty;
            NgayTao = DateTime.Now;
            NoiDung = string.Empty;
        }
    }
}
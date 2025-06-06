namespace AI_Application.Models
{
    public class CauHoi
    {
        public int Id { get; set; }
        public string? TieuDe { get; set; }
        public string? LinhVuc { get; set; }
        public DateTime NgayTao { get; set; }
        public string? NguoiGui { get; set; }
        public bool DaDuyet { get; set; }
    }
}

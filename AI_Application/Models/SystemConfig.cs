using System.ComponentModel.DataAnnotations;

namespace AI_Application.Models
{
    public class SystemConfig
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Key { get; set; }  // ✅ Đánh dấu nullable

        [Required]
        [StringLength(500)]
        public string? Value { get; set; } 

        [StringLength(1000)]
        public string? Description { get; set; }  

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

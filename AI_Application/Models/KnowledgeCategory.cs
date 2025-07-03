using System.ComponentModel.DataAnnotations;

namespace AI_Application.Models
{
    public class KnowledgeCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống.")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }
    }
}

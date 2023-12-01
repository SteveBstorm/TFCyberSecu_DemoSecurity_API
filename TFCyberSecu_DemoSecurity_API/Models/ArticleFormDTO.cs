using System.ComponentModel.DataAnnotations;

namespace TFCyberSecu_DemoSecurity_API.Models
{
    public class ArticleFormDTO
    {
        [Required]
        public string Nom { get; set; }
        [Range(0, 2500)]
        public int Prix { get; set; }
        [Required]
        public string Categorie { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

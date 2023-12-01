using DemoSecurity_DAL.Entities;
using TFCyberSecu_DemoSecurity_API.Models;

namespace TFCyberSecu_DemoSecurity_API.Tools
{
    public static class Mappers
    {
        public static Article ToBLL(this ArticleFormDTO dto)
        {
            return new Article
            {
                Nom = dto.Nom,
                Prix = dto.Prix,
                Description = dto.Description,
                Categorie = dto.Categorie
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs.Food
{
    public class ReadFoodDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public int Preco { get; set; }
        public string TagsDescription { get; set; }
        public string Description { get; set; }
    }
}

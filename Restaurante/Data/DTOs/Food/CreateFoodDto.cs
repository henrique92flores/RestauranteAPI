using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs.Food
{
    public class CreateFoodDto
    {
        [Required] 
        public int RestaurantId { get; set; }
        [Required(ErrorMessage = "O título do prato é obrigatório")]
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TagsId { get; set; }
        [Required]
        [Range(0, 600, ErrorMessage = "O preco deve ser entre 0 600 reais")]
        public int Preco { get; set; }
        //public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}

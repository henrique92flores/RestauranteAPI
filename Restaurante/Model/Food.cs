using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class Food
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do prato é obrigatório")]
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        [Required]
        [Range(0, 600, ErrorMessage = "O preco deve ser entre 0 600 reais")]
        public int Preco { get; set; }
        [Required]
        public int TagsId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public virtual Restaurant Restaurant { get; set; }
    }
}

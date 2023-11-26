using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs
{
    public class ReviewDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Uma descrição é obrigatoria")]
        public string avaliacao { get; set; }
        [Range(0, 5, ErrorMessage = "A nota deve ser entre 0 e 5")]
        [Required(ErrorMessage = "A nota do restaurante é obrigatório")]
        public int nota { get; set; }
        [Required]
        public int RestaurantId { get; set; }
    }
}

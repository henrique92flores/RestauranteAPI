using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class OrderItem
    {
        [Key]
        [Required]
        public int? Id { get; set; }
        [Required(ErrorMessage = "O nome do item é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O preço do item é obrigatório")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "A quantidade do item é obrigatório")]
        public int Quantity { get; set; }
    }
}

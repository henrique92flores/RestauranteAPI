using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class OrderItemDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "O nome do item é obrigatório")]
        public int productid { get; set; }
        [Required(ErrorMessage = "O nome do item é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O preço do item é obrigatório")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "A quantidade do item é obrigatório")]
        public int Quantity { get; set; }
    }
}
using Restaurante.Model;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs
{
    public class OrderDto
    {
        public int? Id {  get; set; }
        public int? RestaurantId { get; set; }
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Ao menos um item é obrigatorio por pedido")]
        public List<OrderItemDto> OrderItemDto { get; set; }
        [Required(ErrorMessage = "O total do pedido é obrigatório")]
        public decimal Total { get; set; }
        public int status { get; set; }
        public int PaymentType { get; set; }
        public int NumeroCartao { get; set; }
        public string? NomeCartao { get; set; }
        public DateTime? dateTime { get; set; }
    }
}

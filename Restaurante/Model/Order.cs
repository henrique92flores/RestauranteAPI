using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public virtual List<OrderItem> Items { get; set; }
        [Required(ErrorMessage = "O total do pedido é obrigatório")]
        public decimal Total { get; set; }
        [Required(ErrorMessage = "O total do pedido é obrigatório")]
        public int status { get; set; }
        public int PaymentType { get; set; }
        public int NumeroCartao { get; set; }
        public string? NomeCartao { get; set; }
        public DateTime dateTime { get; set; }
    }
}

using Restaurante.Model;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs.Restaurant
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do restaurante é obrigatório")]
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Imagem { get; set; }
        public float Avalicao { get; set; }
        [Required]
        public string CNPJ { get; set; }
        [Required]
        public AddresDto AddresDto { get; set; }
        //public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}

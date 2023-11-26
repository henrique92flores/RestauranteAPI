using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs
{
    public class AddresDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Rua { get; set; }
        [Required]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Estado { get; set; }
    }
}

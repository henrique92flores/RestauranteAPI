using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

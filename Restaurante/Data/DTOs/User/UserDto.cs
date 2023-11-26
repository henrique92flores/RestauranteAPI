using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Data.DTOs.User
{
    public class UserDto
    {
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CNPJ/CPF é obrigatório")]
        [MaxLength(14)]
        [MinLength(11)]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O email do cliente é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O ConfirmaSenha é obrigatório")]
        [Compare("Senha")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "O telefone do cliente é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O tipo do cliente é obrigatório")]
        public int TipoCliente { get; set; }
        //[Required(ErrorMessage = "O nome do cliente é obrigatório")]
        //public AddresDto Addres { get; set; }

    }
}

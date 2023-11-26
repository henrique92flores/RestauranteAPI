using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Restaurante.Model
{
    public class User : IdentityUser
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O tipo do cliente é obrigatório")]
        public int TipoUser { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Telefone { get; set; }
        //[Required(ErrorMessage = "O nome do cliente é obrigatório")]
        //public Addres Addres { get; set; }
    }
}

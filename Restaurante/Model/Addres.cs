using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class Addres
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Rua { get; set; }
        [Required]
        [Range(0, 600, ErrorMessage = "O preco deve ser entre 0 600 reais")]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório")]
        public string Estado { get; set; }
        //public virtual Restaurant Restaurant { get; set; }

    }
}

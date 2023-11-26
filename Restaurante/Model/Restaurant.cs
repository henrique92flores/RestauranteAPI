using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class Restaurant
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do restaurante é obrigatório")]
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Imagem { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public float Avalicao { get; set; }
        [Required]
        public int AddresId {  get; set; } 
        [Required]
        public virtual Addres Addres { get; set;}
    }
}

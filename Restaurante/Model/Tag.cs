using System.ComponentModel.DataAnnotations;

namespace Restaurante.Model
{
    public class Tag
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}

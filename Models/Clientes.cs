using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Clientes")]
    public class Clientes : Pessoas
    {
        [Display(Name = "Endereco"), Column("Endereco"), Required]
        public string? Endereco { get; set; }

        [Display(Name = "Telefone"), Column("Telefone"), Required]
        public string? Telefone { get; set; }

        [Display(Name = "E-mail"), Column("Email"), Required]
        public string? Email { get; set; }
    }
}
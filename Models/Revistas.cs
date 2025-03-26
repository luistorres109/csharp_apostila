using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Revistas")]
    public class Revistas : Materiais
    {
        [Display(Name = "Paginas"), Column("Paginas"), Required]
        public int? Paginas { get; set; }

        [Display(Name = "Editora"), Column("Editora"), Required]
        public String? Editora { get; set; }

        [Display(Name = "Edição"), Column("Edicao"), Required]
        public int? Edicao { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Livros")]
    public class Livros : Materiais
    {
        [Display(Name = "Capítulos"), Column("Capitulos"), Required]
        public int? Capitulos { get; set; }

        [Display(Name = "Páginas"), Column("Paginas"), Required]
        public int? Paginas { get; set; }

        [Display(Name = "Editora"), Column("Editora"), Required]
        public String? Editora { get; set; }

        [Display(Name = "Categoia"), Column("Categoia"), Required]
        public String? Categoia { get; set; }
    }
}

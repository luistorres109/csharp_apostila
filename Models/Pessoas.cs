using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Pessoas")]
    public class Pessoas
    {
        [Display(Name = "Código"), Column("id_pessoa")]
        public int Id { get; set; }

        [Display(Name = "Nome"), Column("Nome"), Required]
        public string? Nome { get; set; }

        [Display(Name = "Data de Nascimento"), Column("DataNascimento"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }
    }
}

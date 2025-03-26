using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Materiais")]
    public class Materiais
    {
        [Key]
        [Display(Name = "Código"), Column("id_material")]
        public int Id { get; set; }

        [Display(Name = "Nome"), Column("Nome"), Required]
        public string? Nome { get; set; }

        [Display(Name = "Autor"), Column("id_pessoa"), ForeignKey("Autor")]
        public int? CodigoAutor { get; set; }
        public Autores? Autor { get; set; }

        [Display(Name = "Data de Publicação"), Column("DataPublicacao"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataPublicacao { get; set; }
    }
}
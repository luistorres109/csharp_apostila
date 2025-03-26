using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Itens")]
    public class Itens
    {
        [Key]
        [Display(Name = "Código"), Column("id_item")]
        public int Id { get; set; }

        [Display(Name = "Material"), Column("Material"), ForeignKey("material")]
        public int? CodigoMaterial { get; set; }
        public Materiais? material { get; set; }
    }
}
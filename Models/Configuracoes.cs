using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Configuracao")]
    public class Configuracoes
    {
        [Key]
        [Display(Name = "Código"), Column("id_configuracao")]
        public int Id { get; set; }

        [Display(Name = "Dias de Empréstimo"), Column("dias_emprestimo"), Required]
        public short? DiasEmprestimo { get; set; }

        [Display(Name = "Dias de Lançamento"), Column("dias_lancamento"), Required]
        public short? DiasLancamento { get; set; }

        [Display(Name = "Valor da Multa p/ Dia"), Column("valor_multa"), Required]
        public float? ValorMulta { get; set; }
    }
}

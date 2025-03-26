using BIBLIOTECA_APOSTILA.Data;
using BIBLIOTECA_APOSTILA.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BIBLIOTECA_APOSTILA.Models.Usuarios;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Emprestimos")]
    public class Emprestimos {
        public readonly Contexto? _context;
        public Emprestimos()
        {
            CodigoItem = 0;
            CodigoUsuario = 0;
            CodigoCliente = 0;
        }
        public Emprestimos(Contexto contexto)
        {
            _context = contexto;
        }
        public enum SituacaoEmprestimo {
            Aberto = 0,
            Devolvido = 1,
            Cancelado = 2,
        }
    
        [Key]
        [Display(Name = "Código"), Column("id_emprestimo")]
        public int Id { get; set; }

        [Display(Name = "Usuario"), Column("id_usuario"), ForeignKey("Usuarios")]
        public int? CodigoUsuario { get; set; }
        [ForeignKey("CodigoUsuario")] 
        public Usuarios? Usuario { get; set; }

        [Display(Name = "Cliente"), Column("id_pessoa"), ForeignKey("Clientes")]
        public int? CodigoCliente { get; set; }
        [ForeignKey("CodigoCliente")]
        public Clientes? Cliente { get; set; }

        [Display(Name = "Produto"), Column("CodigoItem"), ForeignKey("Item")]
        public int? CodigoItem { get; set; }
        public Itens? Item { get; set; }

        [Display(Name = "Situação"), Column("Situacao")]
        public SituacaoEmprestimo? Situacao { get; set; }

        [Display(Name = "Data do Empréstimo"), Column("data_emprestimo"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_emprestimo { get; set; }

        [Display(Name = "Data de Devolução"), Column("data_devolucao"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_devolucao { get; set; }

        [Display(Name = "Devolvido dia"), Column("data_pos_devolvida"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_pos_devolvida { get; set; }

        [Display(Name = "Valor da Multa")]
        public double? Multa
        { get
            {
                if (_context != null)
                {
                    var valor = _context.Configuracoes.Find(1);
                    var item = _context.Itens.Find(CodigoItem);
                    var materiais = _context.Materiais.Find(item.CodigoMaterial);
                    var DataDevolvida = DateTime.Today;
                    int multaLancamento = 4;

                    if (Situacao == Emprestimos.SituacaoEmprestimo.Devolvido)
                    {   
                        // inicio material
                        TimeSpan tempMateriais = (TimeSpan)(DateTime.Today - materiais.DataPublicacao);
                        int totalDiasMat = (int)tempMateriais.TotalDays;

                        // fim material
                        TimeSpan intervaloDias = (TimeSpan)(DateTime.Today - data_devolucao);
                        int totalDias = (int)intervaloDias.TotalDays;
                        float? valorTotal = valor.ValorMulta * totalDias;

                        if (DataDevolvida > data_devolucao)
                        {
                            if (totalDiasMat > 0)
                            {
                                return valorTotal * multaLancamento;
                            }
                            return valorTotal;
                        }

                        // inicio material
                        if (data_pos_devolvida < data_devolucao)
                        {
                            TimeSpan datDevolvida = (TimeSpan)(data_devolucao - data_pos_devolvida);
                            int totalDiasDevIgual = (int)datDevolvida.TotalDays;
                            // fim material
                            float? valorTotalDevolvido = valor.ValorMulta * totalDiasDevIgual;
                            return valorTotalDevolvido;
                        }
                        TimeSpan tempDevolvida = (data_devolucao.HasValue && data_pos_devolvida.HasValue)
                            ? (data_devolucao.Value - data_pos_devolvida.Value)
                            : TimeSpan.Zero;
                        int totalDiasDev = (int)tempDevolvida.TotalDays;
                        // fim material
                        float? valorTotalDev = valor.ValorMulta * totalDiasDev;
                        return valorTotalDev;
                    }

                    // inicio material
                    TimeSpan tempMat = (TimeSpan)(DateTime.Today - materiais.DataPublicacao);
                    int totDiasMat = (int)tempMat.TotalDays;
                    // fim material

                    TimeSpan interval = (TimeSpan)(DateTime.Today - data_devolucao);
                    int totDias = (int)interval.TotalDays;
                    float? valorTot = valor.ValorMulta * totDias;

                    if (DataDevolvida > data_devolucao)
                    {
                        if(totDiasMat > 0)
                        {
                            return valorTot * multaLancamento;
                        }
                        return valorTot;
                    }
                }
                return 0;
            }
        }
        public String MultaToString
        {
            get
            {
                return "R$ " + String.Format("{0:0.00}", Multa);
            }
        }

        public string NomeItem
        {
            get
            {
                if (_context != null)
                {
                    var item = _context.Itens.Find(CodigoItem);
                    var material = _context.Materiais.Find(item.CodigoMaterial);
                    var NomeProduto = material.Nome;
                    return " " + NomeProduto;
                }
                return " ";
            }
        }
    }
}
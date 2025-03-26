using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BIBLIOTECA_APOSTILA.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        public enum SituacaoUsuario
        {
            Ocupado = 0,
            Livre = 1,
            Indisponivel = 2,
        }

        [Key]
        [Display(Name = "Código"), Column("id_usuario")]
        public int Id { get; set; }

        [Display(Name = "Login"), Column("Login"), Required]
        public string? Login { get; set; }

        [Display(Name = "Senha"), Column("Senha"), Required]
        public string? Senha { get; set; }

        [Display(Name = "Situação"), Column("SituacaoUsuario")]
        public SituacaoUsuario? Tipo { get; set; }

        [Display(Name = "Funcionário"), Column("id_pessoa"), ForeignKey("Funcionario")]
        public int? CodigoFuncionario { get; set; }
        public Funcionarios? Funcionario { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using BIBLIOTECA_APOSTILA.Models;

namespace BIBLIOTECA_APOSTILA.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {

        }
        public DbSet<BIBLIOTECA_APOSTILA.Models.Pessoas> Pessoas { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Autores> Autores { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Clientes> Clientes { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Funcionarios> Funcionarios { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Usuarios> Usuarios { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Materiais> Materiais { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Apostilas> Apostilas { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Livros> Livros { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Itens> Itens { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Revistas> Revistas { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Configuracoes> Configuracoes { get; set; } = default!;
        public DbSet<BIBLIOTECA_APOSTILA.Models.Emprestimos> Emprestimos { get; set; } = default!;
    }
}

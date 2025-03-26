using BIBLIOTECA_APOSTILA.Data;
using BIBLIOTECA_APOSTILA.Models;

namespace BIBLIOTECA_APOSTILA.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Contexto _context;

        public UsuarioRepositorio(Contexto context)
        {
            _context = context;
        }

        public Usuarios BuscarLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Login == login);
        }

        public void Adicionar(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}

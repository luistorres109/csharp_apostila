using BIBLIOTECA_APOSTILA.Models;

namespace BIBLIOTECA_APOSTILA.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Usuarios BuscarLogin(string login);
        void Adicionar(Usuarios usuario);
    }
}
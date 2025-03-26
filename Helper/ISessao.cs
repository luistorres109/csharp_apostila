using BIBLIOTECA_APOSTILA.Models;
using Newtonsoft.Json;

namespace BIBLIOTECA_APOSTILA.Helper
{
    [JsonObject(IsReference = true)]
    public interface ISessao
    {
        void CriarSessaoUsuario(Usuarios usuario);
        void RemoveSessaoUsuario();
        Usuarios BuscarSessaoUsuario();
    }
}

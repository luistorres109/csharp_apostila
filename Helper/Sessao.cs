using BIBLIOTECA_APOSTILA.Models;
using Newtonsoft.Json;

namespace BIBLIOTECA_APOSTILA.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _HttpContext;

        public Sessao(IHttpContextAccessor HttpContext)
        {
            _HttpContext = HttpContext;
        }

        public Usuarios BuscarSessaoUsuario()
        {
            string SessaoUsuario = _HttpContext.HttpContext.Session.GetString("SessaoUsuarioLogado");

            if (string.IsNullOrEmpty(SessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<Usuarios>(SessaoUsuario);
        }

        public void CriarSessaoUsuario(Usuarios usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);

            _HttpContext.HttpContext.Session.SetString("SessaoUsuarioLogado", valor);
        }

        public void RemoveSessaoUsuario()
        {
           _HttpContext.HttpContext.Session.Remove("SessaoUsuarioLogado");
        }
    }
}

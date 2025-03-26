using BIBLIOTECA_APOSTILA.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BIBLIOTECA_APOSTILA.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("SessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            Usuarios usuario = JsonConvert.DeserializeObject<Usuarios>(sessaoUsuario);

            return View(usuario);
        }
    }
}

using BIBLIOTECA_APOSTILA.Helper;
using BIBLIOTECA_APOSTILA.Models;
using BIBLIOTECA_APOSTILA.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BIBLIOTECA_APOSTILA.Controllers
{
    [JsonObject(IsReference = true)]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoveSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuarios usuario = _usuarioRepositorio.BuscarLogin(loginModel.Login);

                    if (usuario != null && usuario.Tipo == 0)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"A senha de usuário é inválida! Por favor, tente novamente!";
                    }
                    TempData["MensagemErro"] = $"Usuário inválido e/ou inativo! Por favor, tente novamente!";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe de erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}

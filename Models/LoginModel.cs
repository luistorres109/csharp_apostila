using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA_APOSTILA.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite login")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public string? Senha { get; set; }
    }
}

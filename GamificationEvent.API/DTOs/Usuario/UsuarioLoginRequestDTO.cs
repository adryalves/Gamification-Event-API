using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Usuario
{
    public class UsuarioLoginRequestDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Formato de e-mail inválido.")]
        public string Email {  get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Senha { get; set; }
    }
}

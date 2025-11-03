using GamificationEvent.Core.Validações;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Usuario
{
    public class UsuarioUpdateDTO
    {

        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = null!;
 
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [ValidaçãoCPF(ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; } = null!;
        public string? Telefone { get; set; }
        public DateOnly? DataDeNascimento { get; set; }
        public string? Foto { get; set; }

        public List<RedeSocialDTO> RedesSociais { get; set; } = new();
    }
}


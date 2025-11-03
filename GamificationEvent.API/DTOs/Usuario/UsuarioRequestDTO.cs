using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Validações;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GamificationEvent.API.DTOs.Usuario
{
    public class UsuarioRequestDTO
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

        [Required(AllowEmptyStrings = false)]
        public string Senha { get; set; } = null!;

        public string? Telefone { get; set; }
        public DateOnly? DataDeNascimento { get; set; }
        public string? Foto { get; set; }

        public List<RedeSocialDTO> RedesSociais { get; set; } = new();
    }

    public class RedeSocialDTO
    {
        public string Plataforma { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

}


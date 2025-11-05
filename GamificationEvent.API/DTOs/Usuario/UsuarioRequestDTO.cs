using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Validações;
using System.ComponentModel;
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

        [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
ErrorMessage = "O telefone deve estar no formato (XX) XXXXX-XXXX ou XX XXXX-XXXX.")]
        [DefaultValue("(77) 98765-4321")]
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


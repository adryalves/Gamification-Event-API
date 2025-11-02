using GamificationEvent.Core.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GamificationEvent.API.DTOs.Usuario
{
    public class UsuarioRequestDTO
    {

        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Cpf { get; set; } = null!;
        [Required]
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


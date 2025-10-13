using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs
{
    public class UsuarioUpdateDTO
    {
 
            [Required]
            public string Nome { get; set; } = null!;
            [Required]
            [EmailAddress]
            public string Email { get; set; } = null!;
            [Required]
            public string Cpf { get; set; } = null!;
            public string? Telefone { get; set; }
            public string? DataDeNascimento { get; set; } = null!;
            public string? Foto { get; set; }

            public List<RedeSocialDTO> RedesSociais { get; set; } = new();
        }
    }


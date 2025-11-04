using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Palestrante
{
    public class PalestranteUpdateDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Formato de e-mail inválido.")]
        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public string? Profissao { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string? Linkedin { get; set; }

    }
}

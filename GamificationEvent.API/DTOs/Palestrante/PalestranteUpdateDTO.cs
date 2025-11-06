using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
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

        [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "O telefone deve estar no formato (XX) XXXXX-XXXX ou XX XXXX-XXXX.")]
        [DefaultValue("(77) 98765-4321")]
        public string? Telefone { get; set; }

        public string? Profissao { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string? Linkedin { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.PaletaCor
{
    public class CorRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^#([A-Fa-f0-9]{3}|[A-Fa-f0-9]{6})$",
        ErrorMessage = "O código hexadecimal deve estar no formato #RGB ou #RRGGBB.")]
        public string HexCodigo { get; set; } = null!;

        public string? Nome { get; set; }
    }
}

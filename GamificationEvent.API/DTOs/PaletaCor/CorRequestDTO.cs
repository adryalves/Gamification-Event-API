using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.PaletaCor
{
    public class CorRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string HexCodigo { get; set; } = null!;

        public string? Nome { get; set; }
    }
}

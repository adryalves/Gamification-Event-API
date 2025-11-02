using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Premio
{
    public class PremioRequestDTO
    {
        [Required]
        public Guid IdEvento { get; set; }

        public Guid? IdPatrocinador { get; set; }

        [Required]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        public string? Tipo { get; set; }

        public string? InfoResgate { get; set; }

    }
}

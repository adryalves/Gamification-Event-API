using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.SubEvento
{
    public class PerguntasSubEventoRequestDTO
    {
        [Required]
        public Guid IdParticipante { get; set; }

        [Required]
        public Guid IdSubEvento { get; set; }

        public string? Assunto { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Pergunta { get; set; } = null!;

        [Required]
        public DateTime DataHora { get; set; }
    }
}

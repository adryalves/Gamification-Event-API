using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.ParticipantePremio
{
    public class ParticipantePremioRequestDTO
    {
        [Required]
        public Guid IdParticipante { get; set; }

        [Required]
        public Guid IdPremio { get; set; }

        public string? Motivo { get; set; }

        [Required]
        public DateTime DataConcessao { get; set; }
    }
}

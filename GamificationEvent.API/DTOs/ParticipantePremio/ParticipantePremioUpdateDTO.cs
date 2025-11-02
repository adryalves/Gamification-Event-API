using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.ParticipantePremio
{
    public class ParticipantePremioUpdateDTO
    {
        public string? Motivo { get; set; }

        [Required]
        public DateTime DataConcessao { get; set; }
    }
}

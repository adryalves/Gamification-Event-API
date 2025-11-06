using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.SubEvento
{
    public class CheckInSubEventoRequestDTO
    {
        [Required]
        public Guid IdSubEvento { get; set; }

        [Required]
        public Guid IdParticipante { get; set; }

    }
}

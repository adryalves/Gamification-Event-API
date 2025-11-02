namespace GamificationEvent.API.DTOs.ParticipantePremio
{
    public class ParticipantePremioRequestDTO
    {

        public Guid IdParticipante { get; set; }

        public Guid IdPremio { get; set; }

        public string? Motivo { get; set; }

        public DateTime DataConcessao { get; set; }
    }
}

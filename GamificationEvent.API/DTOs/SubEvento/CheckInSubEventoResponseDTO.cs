namespace GamificationEvent.API.DTOs.SubEvento
{
    public class CheckInSubEventoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdSubEvento { get; set; }

        public Guid IdParticipante { get; set; }

        public DateTime DataHora { get; set; }

    }
}

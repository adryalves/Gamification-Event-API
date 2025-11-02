namespace GamificationEvent.API.DTOs.SubEvento
{
    public class PerguntasSubEventoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdSubEvento { get; set; }

        public string? Assunto { get; set; }

        public string Pergunta { get; set; } = null!;

        public DateTime DataHora { get; set; }
    }
}

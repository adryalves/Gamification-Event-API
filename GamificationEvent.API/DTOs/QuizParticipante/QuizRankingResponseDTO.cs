namespace GamificationEvent.API.DTOs.QuizParticipante
{
    public class QuizRankingResponseDTO
    {
        public Guid IdQuiz { get; set; }
        public List<QuizRankingParticipanteResponseDTO> Participantes { get; set; } = new();
    }

    public class QuizRankingParticipanteResponseDTO
    {
        public int Posicao { get; set; }
        public Guid IdParticipante { get; set; }
        public string Nome { get; set; } = null!;
        public int QuantidadeAcertos { get; set; }
        public double TempoTotalRespostas { get; set; }
    }
}

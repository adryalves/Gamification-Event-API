namespace GamificationEvent.API.DTOs.QuizParticipante
{
    public class ParticipanteQuizRespostaRequestDTO
    {
        public Guid IdParticipante { get; set; }

        public Guid IdQuizPergunta { get; set; }

        public Guid IdQuizAlternativa { get; set; }

        public TimeOnly HoraResposta { get; set; }
    }
}

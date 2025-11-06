using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.QuizParticipante
{
    public class ParticipanteQuizRespostaRequestDTO
    {
        [Required]
        public Guid IdParticipante { get; set; }

        [Required]  
        public Guid IdQuizPergunta { get; set; }

        [Required]
        public Guid IdQuizAlternativa { get; set; }

        [Required]
        public TimeOnly HoraResposta { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.QuizParticipante
{
    public class QuizParticipanteRequestDTO
    {
        [Required]
        public Guid IdParticipante { get; set; }

        [Required]
        public Guid IdQuiz { get; set; }
    }
}

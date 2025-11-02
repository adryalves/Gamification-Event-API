using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizPerguntaRequestDTO
    {
        [Required]
        public Guid IdQuiz { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Enunciado { get; set; } = null!;
    }
}

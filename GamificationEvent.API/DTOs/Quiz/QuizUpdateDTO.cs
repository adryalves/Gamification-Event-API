using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizUpdateDTO
    {
        public Guid? IdPatrocinador { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        public string? Tema { get; set; }

        [Required]
        public DateTime DataQuiz { get; set; }

        [Required]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        public TimeOnly HoraFim { get; set; }
    }
}

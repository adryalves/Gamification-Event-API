using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizPerguntaUpdateDTO
    {
        [Required]
        public string Enunciado { get; set; } = null!;

    }
}

namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizUpdateDTO
    {
        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Tema { get; set; }

        public DateTime DataQuiz { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }
    }
}

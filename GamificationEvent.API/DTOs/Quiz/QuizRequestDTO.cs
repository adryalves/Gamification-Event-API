namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizRequestDTO
    {
        public Guid IdEvento { get; set; }

        public Guid? IdSubEvento { get; set; }

        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Tema { get; set; }

        public DateTime DataQuiz { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

    }
}

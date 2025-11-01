namespace GamificationEvent.API.DTOs
{
    public class QuizResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid? IdSubEvento { get; set; }

        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Tema { get; set; }

        public DateTime DataQuiz { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        public bool Deletado { get; set; }
    }
}

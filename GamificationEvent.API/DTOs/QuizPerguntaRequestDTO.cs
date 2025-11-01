namespace GamificationEvent.API.DTOs
{
    public class QuizPerguntaRequestDTO
    {
        public Guid IdQuiz { get; set; }

        public string Enunciado { get; set; } = null!;
    }
}

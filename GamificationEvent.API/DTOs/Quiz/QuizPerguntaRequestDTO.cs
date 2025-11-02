namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizPerguntaRequestDTO
    {
        public Guid IdQuiz { get; set; }

        public string Enunciado { get; set; } = null!;
    }
}

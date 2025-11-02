namespace GamificationEvent.API.DTOs.Quiz
{
    public class AlternativasPerguntasQuizDTO
    {
        public Guid IdPerguntaQuiz { get; set; }
        public List<AlternativaQuizDTO> AlternativaQuizDTOs { get; set; } = new List<AlternativaQuizDTO>();
    }

    public class AlternativaQuizDTO
    {
        public string Resposta { get; set; } = null!;
        public bool ECorreta { get; set; }

    }
}

using System.Text.Json.Serialization;

namespace GamificationEvent.API.DTOs.Quiz
{
    public class QuizPerguntasEAlternativasResponseDTO
    {

        public Guid IdQuiz { get; set; }
        public List<QuizPerguntaCompletaResponseDTO> Perguntas { get; set; } = new List<QuizPerguntaCompletaResponseDTO>();

    }

    public class QuizPerguntaCompletaResponseDTO
    {
        public Guid Id { get; set; }

        public string Enunciado { get; set; } = null!;

        public bool Deletado { get; set; }

        public List<QuizAlternativasCompletasResponseDTO> PerguntaAlternativas { get; set; } = new List<QuizAlternativasCompletasResponseDTO>();
    }

    public class QuizAlternativasCompletasResponseDTO
    {
        public Guid Id { get; set; }

        public string Resposta { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ECorreta { get; set; }

        public bool Deletado { get; set; }
    }
}


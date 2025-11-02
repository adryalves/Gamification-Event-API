namespace GamificationEvent.API.DTOs.QuizParticipante
{
    public class QuizParticipanteResultadoResponseDTO
    {
        public Guid IdQuiz { get; set; }
        public Guid IdParticipante { get; set; }
        public string NomeQuiz { get; set; } = null!;
        public int QuantidadeAcertos { get; set; }
        public int QuantidadePerguntas { get; set; }
        public List<PerguntaRespondidaResponseDTO> Perguntas { get; set; } = new();
    }

    public class PerguntaRespondidaResponseDTO
    {
        public Guid IdPergunta { get; set; }
        public string Enunciado { get; set; } = null!;
        public string RespostaEscolhida { get; set; } = null!;
        public bool EstaCorreta { get; set; }
    }
}

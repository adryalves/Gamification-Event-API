using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Models
{
    public class QuizParticipanteResultadoModel
    {
        public Guid IdQuiz { get; set; }
        public Guid IdParticipante { get; set; }
        public string NomeQuiz { get; set; } = null!;
        public int QuantidadeAcertos { get; set; }
        public int QuantidadePerguntas { get; set; }
        public List<PerguntaRespondidaModel> Perguntas { get; set; } = new();
    }

    public class PerguntaRespondidaModel
    {
        public Guid IdPergunta { get; set; }
        public string Enunciado { get; set; } = null!;
        public string RespostaEscolhida { get; set; } = null!;
        public bool EstaCorreta { get; set; }
    }
}


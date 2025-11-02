using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Models
{
    public class QuizRankingModel
    {
        public Guid IdQuiz { get; set; }
        public List<QuizRankingParticipanteModel> Participantes { get; set; } = new();
        public int TotalParticipantes => Participantes.Count;
    }

    public class QuizRankingParticipanteModel
    {
        public Guid IdParticipante { get; set; }
        public string Nome { get; set; } = null!;
        public int QuantidadeAcertos { get; set; }
        public double TempoTotalRespostas { get; set; }
        public int Posicao { get; set; }
    }
}


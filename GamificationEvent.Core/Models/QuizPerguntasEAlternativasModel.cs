using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Models
{
    public class QuizPerguntasEAlternativasModel
    {
        public Guid IdQuiz { get; set; }
        public List<QuizPerguntaCompletaModel> Perguntas { get; set; } = new List<QuizPerguntaCompletaModel>();

    }

    public class QuizPerguntaCompletaModel
    {
        public Guid Id { get; set; }

        public string Enunciado { get; set; } = null!;

        public bool Deletado { get; set; }

        public List<QuizAlternativasCompletasModel> PerguntaAlternativas { get; set; } = new List<QuizAlternativasCompletasModel>();
    }

    public class QuizAlternativasCompletasModel
    {
        public Guid Id { get; set; }

        public string Resposta { get; set; } = null!;

        public bool? ECorreta { get; set; }

        public bool Deletado { get; set; }
    }
}

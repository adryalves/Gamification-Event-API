using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class QuizPerguntasEAlternativas
    {
        public Guid IdQuiz {  get; set; }
        public List<QuizPerguntaCompleta> Perguntas { get; set; } = new List<QuizPerguntaCompleta>();   

    }

    public class QuizPerguntaCompleta
    {
        public Guid Id { get; set; }

        public string Enunciado { get; set; } = null!;

        public bool Deletado { get; set; }

        public List<QuizAlternativasCompletas> PerguntaAlternativas { get; set; } = new List<QuizAlternativasCompletas>();
    }

    public class QuizAlternativasCompletas
    {
        public Guid Id { get; set; }

        public string Resposta { get; set; } = null!;

        public bool? ECorreta { get; set; }

        public bool Deletado { get; set; }
    }
}

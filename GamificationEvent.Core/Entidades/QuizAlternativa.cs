using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class QuizAlternativa
    {
        public Guid Id { get; set; }

        public Guid IdQuizPergunta { get; set; }

        public string Resposta { get; set; } = null!;

        public bool ECorreta { get; set; }

        public bool Deletado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class QuizPergunta
    {
        public Guid Id { get; set; }

        public Guid IdQuiz { get; set; }

        public string Enunciado { get; set; } = null!;

        public bool Deletado { get; set; }

    }
}

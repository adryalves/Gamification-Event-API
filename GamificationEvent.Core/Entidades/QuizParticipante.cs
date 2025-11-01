using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class QuizParticipante
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdQuiz { get; set; }
    }
}

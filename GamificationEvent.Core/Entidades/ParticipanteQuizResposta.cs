using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class ParticipanteQuizResposta
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdQuizPergunta { get; set; }

        public Guid IdQuizAlternativa { get; set; }

        public TimeOnly HoraResposta { get; set; }
    }
}

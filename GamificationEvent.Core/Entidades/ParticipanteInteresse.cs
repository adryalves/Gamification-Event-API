using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class ParticipanteInteresse
    {
        public Guid Id { get; set; }
        public Guid IdInteresse { get; set; }
        public Guid IdParticipante { get; set; }

        public Participante Participante { get; set; }
    }
}

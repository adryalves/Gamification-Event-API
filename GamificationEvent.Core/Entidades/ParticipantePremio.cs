using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class ParticipantePremio
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdPremio { get; set; }

        public string? Motivo { get; set; }

        public DateTime DataConcessao { get; set; }
    }
}

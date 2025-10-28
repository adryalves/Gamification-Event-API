using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class CheckInSubEvento
    {
        public Guid Id { get; set; }

        public Guid IdSubEvento { get; set; }

        public Guid IdParticipante { get; set; }

        public DateTime DataHora { get; set; }

        public CheckInSubEvento(Guid idSubEvento, Guid idParticipante)
        {
            IdSubEvento = idSubEvento;
            IdParticipante = idParticipante;
        }

        public CheckInSubEvento()
        {
        }
    }


}

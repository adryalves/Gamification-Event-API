using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class DesafioParticipante
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdDesafio { get; set; }

        public Status_Desafio StatusDesafio { get; set; }

        public int QuantidadeRealizada { get; set; }

        public DateTime? DataHoraConclusao { get; set; }

        
    }
}

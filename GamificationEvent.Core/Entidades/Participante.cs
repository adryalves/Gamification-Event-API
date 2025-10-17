using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Participante
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid IdUsuario { get; set; }

        public Cargo Cargo { get; set; } 

        public int Pontuacao { get; set; }

        public bool? PrimeiroParticipante { get; set; }

        public DateTime DataHoraCriacao { get; set; }

        public List<ParticipanteInteresse> ParticipanteInteresses { get; set; } = new();
    }
}

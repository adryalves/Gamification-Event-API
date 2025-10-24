using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class PerguntasSubEvento
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdSubEvento { get; set; }

        public string? Assunto { get; set; }

        public string Pergunta { get; set; } = null!;

        public DateTime DataHora { get; set; }
    }
}

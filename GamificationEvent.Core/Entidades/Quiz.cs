using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Quiz
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid? IdSubEvento { get; set; }

        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Tema { get; set; }

        public DateTime DataQuiz { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        public bool Deletado { get; set; }
    }
}

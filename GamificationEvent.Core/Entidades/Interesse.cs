using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Interesse
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public string Nome { get; set; } = null!;

        public bool Deletado { get; set; }
    }
}

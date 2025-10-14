using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Cor
    {
        public Guid Id { get; set; }
        public string HexCodigo { get; set; } = null!;
        public string? Nome { get; set; }

        public Cor()
        {
        }
    }
}

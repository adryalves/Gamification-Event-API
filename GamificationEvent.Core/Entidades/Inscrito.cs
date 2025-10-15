using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Inscrito
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; } = null!;
        public Guid IdEvento { get; set; }
        public string Nome { get; set; } = null!;
        public Cargo Cargo { get; set; }
    }
}

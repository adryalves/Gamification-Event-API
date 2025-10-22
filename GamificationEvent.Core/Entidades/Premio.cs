using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Premio
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        public string? Tipo { get; set; }

        public string? InfoResgate { get; set; }

        public bool Deletado { get; set; }

    }
}

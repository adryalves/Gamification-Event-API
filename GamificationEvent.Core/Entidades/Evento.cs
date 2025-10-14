using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Evento
    {
        public Guid Id { get; set; }

        public Guid IdPaleta { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descricao { get; set; } = null!;

        public string Objetivo { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public string PublicoAlvo { get; set; } = null!;

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

        public bool Deletado { get; set; }
    }
}

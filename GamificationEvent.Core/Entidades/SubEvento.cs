using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class SubEvento
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid? IdPontoMapa { get; set; }

        public string Nome { get; set; } = null!;

        public string? LocalSubEvento { get; set; }

        public string? Assunto { get; set; }

        public string? Tipo { get; set; }

        public string? Categoria { get; set; }

        public Modalidade Modalidade { get; set; }

        public DateTime DataSubEvento { get; set; }

        public TimeOnly HorarioInicio { get; set; }

        public TimeOnly? HorarioFim { get; set; }

        public string? CodigoCheckin { get; set; }

        public bool Deletado { get; set; }

        public List<PalestrantesSubEvento> Palestrantes {  get; set; } = new();
    }

    public class PalestrantesSubEvento
    {
        public Guid Id { get; set; }
        public Guid IdPalestrante { get; set;}
    }
}

using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Desafio
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        public string? Regra { get; set; }

        public int Pontuacao { get; set; }

        public Tipo_Desafio TipoDesafio { get; set; }

        public int QuantidadeDesafio { get; set; }

        public DateTime? DataHoraInicio { get; set; }

        public DateTime? DataHoraFim { get; set; }

        public bool Deletado { get; set; }
    }
}

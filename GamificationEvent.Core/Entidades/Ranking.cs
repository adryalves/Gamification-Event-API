using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class Ranking
    {
        public int Posicao { get; set; }
        public Guid IdParticipante { get; set; }
        public string? Foto { get; set; }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public string Email { get; set; } = null!;
    }
}

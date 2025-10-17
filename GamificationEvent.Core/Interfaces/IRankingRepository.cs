using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IRankingRepository
    {
        Task<List<Ranking>> GetRankingGeralPorIdEvento(Guid idEvento, int quantidade);

        Task<List<Ranking>> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, int quantidade);

    }
}

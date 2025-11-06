using GamificationEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IRankingRepository
    {
        Task<List<RankingModel>> GetRankingGeralPorIdEvento(Guid idEvento, int quantidade);

        Task<List<RankingModel>> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, int quantidade);

    }
}

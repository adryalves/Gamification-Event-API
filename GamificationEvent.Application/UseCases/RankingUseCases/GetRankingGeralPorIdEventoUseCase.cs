using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.RankingUseCases
{
    public class GetRankingGeralPorIdEventoUseCase
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetRankingGeralPorIdEventoUseCase(IRankingRepository rankingRepository, IEventoRepository eventoRepository)
        {
            _rankingRepository = rankingRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<List<Ranking>> GetRankingGeralPorIdEvento(Guid idEvento, int quantidade)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) throw new Exception("Esse evento não existe");

            return await _rankingRepository.GetRankingGeralPorIdEvento(idEvento, quantidade);
        }
    }
}

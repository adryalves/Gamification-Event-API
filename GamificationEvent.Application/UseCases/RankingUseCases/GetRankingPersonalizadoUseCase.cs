using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.RankingUseCases
{
    public class GetRankingPersonalizadoUseCase
    {
        private readonly IRankingRepository _rankingRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetRankingPersonalizadoUseCase(IRankingRepository rankingRepository, IParticipanteRepository participanteRepository, IEventoRepository eventoRepository)
        {
            _rankingRepository = rankingRepository;
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<List<Ranking>> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, int quantidade)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) throw new Exception("Esse evento não existe");

            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) throw new Exception("Esse participante não existe");

            return await _rankingRepository.GetRankingPersonalizado(idEvento, idParticipante, quantidade);
        }
    }
}

using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;
using GamificationEvent.Core.Resultados;
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

        public async Task<Resultado<List<RankingModel>>> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, int quantidade)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<RankingModel>>.Falha($"Esse evento não foi encontrado.");


            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<List<RankingModel>>.Falha($"Esse participante não foi encontrado");

            var resultado = await _rankingRepository.GetRankingPersonalizado(idEvento, idParticipante, quantidade);
            return Resultado<List<RankingModel>>.Ok(resultado);

        }
    }
}

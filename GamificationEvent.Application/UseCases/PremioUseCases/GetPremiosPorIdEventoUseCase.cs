using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PremioUseCases
{
    public class GetPremiosPorIdEventoUseCase
    {
        private readonly IPremioRepository _premioRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetPremiosPorIdEventoUseCase(IPremioRepository premioRepository, IEventoRepository eventoRepository)
        {
            _premioRepository = premioRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Premio>>> GetPremiosPorIdEvento(Guid idEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<Premio>>.Falha($"O id {idEvento} não corresponde a um evento existente");

            var premios = await _premioRepository.GetPremiosPorIdEvento(idEvento);
            return Resultado<List<Premio>>.Ok(premios);
        }
    }
}

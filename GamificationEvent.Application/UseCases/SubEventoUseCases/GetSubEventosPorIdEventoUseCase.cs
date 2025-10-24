using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.SubEventoUseCases
{
    public class GetSubEventosPorIdEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ISubEventoRepository _subEventoRepository;

        public GetSubEventosPorIdEventoUseCase(IEventoRepository eventoRepository, ISubEventoRepository subEventoRepository)
        {
            _eventoRepository = eventoRepository;
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<List<SubEvento>>> GetSubEventosPorIdEvento(Guid idEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<SubEvento>>.Falha($"O id {idEvento} não corresponde a um evento existente");

            var resultado = await _subEventoRepository.GetSubEventosPorIdEvento(idEvento);
            return Resultado<List<SubEvento>>.Ok(resultado);
        }
    }
}

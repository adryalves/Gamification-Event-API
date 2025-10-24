using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PalestranteUseCases
{
    public class GetPalestrantesPorIdEventoUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetPalestrantesPorIdEventoUseCase(IPalestranteRepository palestranteRepository, IEventoRepository eventoRepository)
        {
            _palestranteRepository = palestranteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Palestrante>>> GetPalestrantesPorIdEvento(Guid idEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<Palestrante>>.Falha($"O id {idEvento} não corresponde a um evento existente");

            var palestrantes = await _palestranteRepository.GetPalestrantesPorIdEvento(idEvento);
            return Resultado<List<Palestrante>>.Ok(palestrantes);
        }
    }
}

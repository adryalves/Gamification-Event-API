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
    public class GetPalestrantesPorIdSubEventoUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly ISubEventoRepository _subEventoRepository;

        public GetPalestrantesPorIdSubEventoUseCase(IPalestranteRepository palestranteRepository, ISubEventoRepository subEventoRepository)
        {
            _palestranteRepository = palestranteRepository;
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<List<Palestrante>>> GetPalestrantesPorIdSubEvento(Guid idSubEvento)
        {
            var subEvento = await _subEventoRepository.GetSubEventoPorId(idSubEvento);
            if (subEvento == null) return Resultado<List<Palestrante>>.Falha("Não existe um subEvento Válido com esse Id");

            var palestrantes = await _palestranteRepository.GetPalestrantesPorIdSubEvento(idSubEvento);
            return Resultado<List<Palestrante>>.Ok(palestrantes);

        }
    }
}
